﻿using AppChamaGas.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppChamaGas.Services
{
    public class Base_Service
    {
        public const string URL_VIACEP = "https://viacep.com.br/ws/";
        public const string URL_MINHAAPI = "https://www.minhaapi.com.br/api/";

        string base_url;


        public Base_Service(string base_url)
        {
            this.base_url = base_url;
        }
        protected HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(base_url);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.Timeout = new TimeSpan(0,2,0);

            return client;
        }

        public virtual async Task<T> Get<T>(string input)
        {
            var client = GetClient();

            var retorno = await client.GetAsync($"{input}/json/");            
            var retornoTexto = await retorno.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(retornoTexto);
        }
    }
}
