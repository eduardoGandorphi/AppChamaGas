using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppChamaGas.Services
{
    public class ReqRes_Service : Base_Service
    {
        protected const string base_url = "https://reqres.in/api/";
        string api_name;
        public ReqRes_Service(string api_name) : base(base_url)
        {
            this.api_name = api_name;
        }

        public override async Task<T> Get<T>(string input)
        {
            var client = base.GetClient();
            var retorno = await client.GetAsync($"{api_name}?page={input}");            
            var retornoTexto = await retorno.Content.ReadAsStringAsync();
            T obj_Convertido = JsonConvert.DeserializeObject<T>(retornoTexto);
            return obj_Convertido;
        }
    }
}
