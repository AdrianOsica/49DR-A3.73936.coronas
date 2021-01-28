using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace coronas
{

    class Program
    {

        private static readonly HttpClient client = new HttpClient();
        
        static async Task Main(string[] args)
        {

            using var client = new HttpClient();

            string content = await client.GetStringAsync("https://restcountries.eu/rest/v2/name/Poland");
           
        
            dynamic stuff = JsonConvert.DeserializeObject(content);
            Console.WriteLine(stuff[0].capital);
        

            //string state;
            //Console.WriteLine("Podaj Panstwo");
            //state = Console.ReadLine();


        }

    }
}
