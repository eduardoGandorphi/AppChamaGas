using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AppChamaGas.Services
{
    public class Base_Services 
    {
        public const string BASE_URL = "https://viacep.com.br/ws/";
        private HttpClient GetClient()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.Timeout = new TimeSpan(0, 0, 5, 0, 0);
            //add token
            // client.DefaultRequestHeaders.Add("token", token);
            return client;
        }

        public async Task<T> Get<T>(string parametro)
        {
            var cliente = GetClient();

            var response = await cliente.GetAsync($"{parametro}/json/");

            var retornoTexto = await response.Content.ReadAsStringAsync();
            var md = JsonConvert.DeserializeObject<T>(retornoTexto);

            return md;
        }
    }
}
