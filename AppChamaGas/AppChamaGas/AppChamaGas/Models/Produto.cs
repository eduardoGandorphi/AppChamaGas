using AppChamaGas.Interface;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppChamaGas.Models
{
    public class Produto : IAzureTabela
    {
        public string Id { get; set; }
        public string FornecedorId { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public string Foto { get; set; }
        public string Unidade { get; set; }

        [JsonIgnore, Ignore]
        public string FornecedorNome { get; set; }
        [JsonIgnore, Ignore]
        public string Distancia { get; set; }
        [JsonIgnore, Ignore]
        public double Latitude { get; set; }
        [JsonIgnore, Ignore]
        public double Longitude { get; set; }


    }
}
