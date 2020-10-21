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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //StorageFile storageFile;
        //StorageFolder storageFolder;
        private readonly JsonService JsonService = new JsonService();
        private readonly CsvService CsvService = new CsvService();



        public MainPage()
        {
            this.InitializeComponent();

            //JsonService.WriteToFileJson().GetAwaiter();
            //CreateAFileCsv().GetAwaiter();
            //ReadFromFileCsv().GetAwaiter();


            //ReadFromFileJson().GetAwaiter();
            //CsvService.WriteToFileCsv().GetAwaiter();


        }


        private async Task ReadFromFileJson()
        {
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
            else
            {
                // do something
            }

        }

        private async Task ReadFromFileCsv() // IEnumerable eller List
        {
            CreateAFileCsv().GetAwaiter();

            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;

            picker.FileTypeFilter.Add(".csv");
            StorageFile file = await picker.PickSingleFileAsync();



            string texts = await FileIO.ReadTextAsync(file);

            
            var parsedResult = new List<Person>();
            
            if(parsedResult != null)
            {
                var records = texts.Split(',');
                parsedResult.Add(new Person(records[0], records[1], Convert.ToInt32(records[2]),records[3]));
            }
            personList.ItemsSource = parsedResult;
        }

        private async Task CreateAFileCsv()
        {
            var content = "Tomas,Bengtsson,47,Västerås";
            var file = await KnownFolders.DocumentsLibrary.CreateFileAsync("mycsv.csv");
            await FileIO.AppendTextAsync(file, content);
        }



        
    }
}
