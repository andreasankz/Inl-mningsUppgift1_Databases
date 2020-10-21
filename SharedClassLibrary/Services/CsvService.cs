using SharedClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace SharedClassLibrary.Services
{
    public class CsvService
    {

        public async Task CreateFileCsv(string filepath, string content)
        {
            var file = await KnownFolders.DocumentsLibrary.CreateFileAsync(filepath);
            await FileIO.AppendTextAsync(file, content);
        }


        public static IEnumerable<Person> ReadFromFile(string filepath, char delimiter = ';') // IEnumerable eller List
        {
            var lines = File.ReadAllLines(filepath).ToList();
            var persons = new List<Person>();

            foreach (var line in lines)
            {
                var data = line.Split(delimiter);

                persons.Add(new Person(data[0], data[1], Convert.ToInt32(data[2]), data[3]));


            }
            return persons;
        }




        //public async Task WriteToFileCsv()
        //{
        //    var person = new Person()
        //    {
        //        FirstName = "Kalle",
        //        LastName = "Olsson",
        //        Age = 20,
        //        City = "Stockholm"
        //    };
        //    List<Person> listOfPerson = new List<Person>();




        //    var file = await KnownFolders.DocumentsLibrary.CreateFileAsync("mycsv.csv");
        //    await FileIO.WriteTextAsync(file, );
        //}

    }
}
