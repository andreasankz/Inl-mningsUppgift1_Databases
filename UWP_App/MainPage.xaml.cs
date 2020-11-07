using SharedClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using SharedClassLibrary.Services;
using Windows.Storage.Streams;
using Windows.ApplicationModel;
using System.Net.Http.Headers;
using System.Xml.Linq;
using System.ServiceModel.Channels;
using Windows.UI.Xaml.Documents;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void btnReadJson_Click(object sender, RoutedEventArgs e)
        {
            //Skapar en jsonfile i App. Codebehind
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add(".json");

            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                string text = await FileIO.ReadTextAsync(file);


                List<Person> jsonDeserialize = JsonConvert.DeserializeObject<List<Person>>(text);

                personList.ItemsSource = jsonDeserialize;
            }
           
        }

        private async void btnReadCsv_Click(object sender, RoutedEventArgs e)
        {

            var csvFilePath = Directory.GetCurrentDirectory() + @"\CSVPersonList.csv";
            var csvData = await File.ReadAllLinesAsync(csvFilePath);

            var parsedResult = new List<Person>();

            foreach (var line in csvData)
            {
                var lines = line.Split(",");

                parsedResult.Add(new Person { FirstName = lines[0], LastName = lines[1], Age = Convert.ToInt32(lines[2]), City = lines[3] });
            }
            personList.ItemsSource = parsedResult;
           
        }

        private void btnReadXml_Click(object sender, RoutedEventArgs e)
        {
            string XMLFilePath = Path.Combine(Package.Current.InstalledLocation.Path, "XMLPersonList.xml");
            XDocument loadedFile = XDocument.Load(XMLFilePath);

            var data = from query in loadedFile.Descendants("person")
                       select new Person
                       {
                           FirstName = (string)query.Element("firstname"),
                           LastName = (string)query.Element("lastname"),
                           Age = (int)query.Element("age"),
                           City = (string)query.Element("city")
                       };
            personList.ItemsSource = data;
        }
    }
}
