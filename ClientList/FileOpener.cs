using CsvHelper.Configuration;
using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClientList
{
    internal class FileOpener
    {
        public void LoadingJsonFile(string fileName)
        {
            Console.WriteLine("To jest plik o rozszerzeniu .json.");
            string jsonString = File.ReadAllText(fileName);
            var clients = JsonConvert.DeserializeObject<List<Client>>(jsonString);
            UserPresentation(clients);
        }

        public void LoadingXmlFile(string fileName)
        {
            Console.WriteLine("To jest plik o rozszerzeniu .xml.");
            string xml = File.ReadAllText(fileName);
            List<Client> clients = new List<Client>();
            using (StringReader stringReader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Client>));
                clients = (List<Client>)serializer.Deserialize(stringReader);
            }
            UserPresentation(clients);
        }

        public void LoadingCsvFile(string fileName)
        {
            Console.WriteLine("To jest plik o rozszerzeniu .csv.");
            var config = new CsvConfiguration(cultureInfo: CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };

            List<Client> clients = new List<Client>();
            using var reader = new StreamReader(fileName);
            using (var csv = new CsvReader(reader, config))
            {
                clients = csv.GetRecords<Client>().ToList();
            }
            UserPresentation(clients);
        }

        public void UserPresentation(List<Client> clients)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                Console.WriteLine(clients[i]);
            }
        }

        public void ChooseLoadingFile()
        {
            Console.WriteLine("Podaj nazwę pliku wraz z rozszerzeniem: ");
            string fileName = Console.ReadLine();
            //var dsada = "." + fileName.Split(".")[fileName.Split(".").Length - 1]; tegp nieee
            if (File.Exists(fileName)) //pattern do pliku "*.txt" => .+\.txt$
            {
                if (fileName.EndsWith(".json")) // można też tak: (Path.GetExtension(fileName) == ".json")
                {
                    LoadingJsonFile(fileName);
                }
                else if (fileName.EndsWith(".xml"))
                {
                    LoadingXmlFile(fileName);
                }
                else if (fileName.EndsWith(".csv"))
                {
                    LoadingCsvFile(fileName);
                }
                else
                {
                    Console.WriteLine("Brak obsługi takiego rozszerzenia.");
                }
            }
            else
            {
                Console.WriteLine("Nie ma takiego pliku.");
            }
        }
    }
}
