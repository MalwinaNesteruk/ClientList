using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClientList
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

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

        public override string ToString()
        {
            return Id + ". " + Name + " " + Surname + ", " + Email;
        }

        public void ChooseLoadingFile()
        {
            Console.WriteLine("Podaj nazwę pliku wraz z rozszerzeniem: ");
            string fileName = Console.ReadLine();
            //var dsada = "." + fileName.Split(".")[fileName.Split(".").Length - 1];
            var dsadas = Path.GetExtension(fileName);

            var sciezka = @"C:\Users\Malwina\source\repos\ClientList\ClientList\bin\Debug\net6.0\clients.xml";


            if (File.Exists(fileName)) //pattern do pliku "*.txt" => .+\.txt$
            {
                if (fileName.EndsWith(".json")) // == "clients.json")
                {
                    LoadingJsonFile(fileName);
                }
                else if (fileName.EndsWith(".xml")) // == "clients.xml")
                {
                    LoadingXmlFile(fileName);
                }
                else if (fileName.EndsWith(".csv")) // == "clients.csv")
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
