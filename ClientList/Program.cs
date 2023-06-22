using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.WebSockets;
using System.Xml;
using System.Xml.Serialization;

namespace ClientList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(nameFile);
            Client client = new Client();
            client.ChooseLoadingFile();
            Console.ReadKey();
        }
    }
}