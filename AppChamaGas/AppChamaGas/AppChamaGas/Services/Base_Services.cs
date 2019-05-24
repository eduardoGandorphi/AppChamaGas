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
        //public const string BASE_URL = "https://viacep.com.br/ws/";
        public const string BASE_URL = "https://reqres.in/api/users";
        
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

        private StringContent GetBody<T>(T model)
        {
            var json_string = JsonConvert.SerializeObject(model);
            return new StringContent(json_string, Encoding.UTF8, "application/json");
        }

        public async Task<T> Get<T>(string parametro)
        {
            var cliente = GetClient();

            var response = await cliente.GetAsync($"{parametro}/json/");

            var retornoTexto = await response.Content.ReadAsStringAsync();
            var md = JsonConvert.DeserializeObject<T>(retornoTexto);

            return md;
        }

        public async Task<T> Post<T>(T model)
        {
            var cliente = GetClient();
            var body = GetBody(model);

            var response = await cliente.PostAsync("", body);

            var retornoTexto = await response.Content.ReadAsStringAsync();
            var md = JsonConvert.DeserializeObject<T>(retornoTexto);

            return md;
        }
    }
}
