using SharedClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Windows.Storage;


namespace SharedClassLibrary.Services
{
    

    public class JsonService
    {

        public async Task WriteToFileJson()
        {
            List<Person> listOfPersons = new List<Person>();
            Person person = new Person()
            {
                FirstName = "Petter",
                LastName = "Johansson",
                Age = 30,
                City = "Örebro"
            };
            listOfPersons.Add(person);
            listOfPersons.Add(new Person("Kalle","Gustavsson",45,"Stockholm"));
            listOfPersons.Add(new Person("Lotta", "Karlsson", 36, "Göteborg"));

            string json = JsonConvert.SerializeObject(listOfPersons);


            var file = await KnownFolders.DocumentsLibrary.CreateFileAsync("myjson.json");
            await FileIO.AppendTextAsync(file, json);
            
        }
    }
}
