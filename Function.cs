/* using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Functions
{
    public class Function
    {
        private static readonly HttpClient client = new HttpClient();
        
        public static async Task<dynamic> capitol(string country)
        {      
            using var client = new HttpClient();
            string get_capitol = await client.GetStringAsync($"https://restcountries.eu/rest/v2/name/{country}");
            dynamic capitol_api = JsonConvert.DeserializeObject(get_capitol);
            return capitol_api[0].capital;
        }
    
    
    }
}
*/