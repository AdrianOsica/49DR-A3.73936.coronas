using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;

namespace coronas
{

    class Program
    {

        private static readonly HttpClient client = new HttpClient();
        
        static async Task Main(string[] args)
        {

            using var client = new HttpClient();

            Console.WriteLine("Podaj Panstwo");
            string country = Console.ReadLine();
            
            string get_capitol = await client.GetStringAsync($"https://restcountries.eu/rest/v2/name/{country}");
            Console.Clear();
        
            dynamic capitol_api = JsonConvert.DeserializeObject(get_capitol);
            
            string get_weather = await client.GetStringAsync($"http://api.openweathermap.org/data/2.5/weather?q={capitol_api[0].capital}&appid=4d9a35918230b98cdfd37d30dca37ca3");
            dynamic weather = JsonConvert.DeserializeObject(get_weather);

            string get_covid = await client.GetStringAsync($"https://api.covid19api.com/live/country/{country}/status/confirmed");
            dynamic covid = JsonConvert.DeserializeObject(get_covid);

            Console.WriteLine($"Stolica kraju ktorego wybrales jest {capitol_api[0].capital}");
            Console.WriteLine($"Szerokosc i dlugosc geograficzna: {covid[14].Lat} {covid[14].Lon} \n \n ");

            Console.WriteLine($"W {capitol_api[0].capital} jest obecnie {weather.weather[0].main} i potwierdzonych zakazen covid-19 {covid[14].Confirmed}");

            Console.Write("Czy chcesz dostac te informacje na maila? (Tak/Nie)");
            string choose = Console.ReadLine();

            if(choose=="Tak"){

                Console.Write("Podaj email \n ");
                string mail = Console.ReadLine();

                var fromAddress = new MailAddress("wsm@zonegames.pl");
                var toAddress = new MailAddress(mail);
                const string fromPassword = "";
                const string subject = "wsm projekt";
                string body = $"W {capitol_api[0].capital} jest obecnie {weather.weather[0].main} i potwierdzonych zakazen covid-19 {covid[14].Confirmed}";

                var smtp = new SmtpClient
                {
                    Host = "smtp.zoho.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                    Console.WriteLine("Wyslano mail");
                }

            }


          

        }

    }
}
