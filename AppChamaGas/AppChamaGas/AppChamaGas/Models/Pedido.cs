using AppChamaGas.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppChamaGas.Models
{
    public class Pedido : IAzureTabela
    {
        public string Id { get; set; }
        public string ClienteId { get; set; }
        public string FornecedorId { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataAgenda { get; set; }
        public DateTime DataEntrega { get; set; }

        //Metodo construtor
        public Pedido(string clienteId, string fornecedorId)
        {
            ClienteId = clienteId;
            FornecedorId = fornecedorId;
            DataEmissao = DateTime.Now;
            DataAgenda = DateTime.Now.AddHours(3);
        }
    }
}
