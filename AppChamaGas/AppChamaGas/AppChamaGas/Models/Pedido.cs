using AppChamaGas.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using AppChamaGas.ViewModel;

namespace AppChamaGas.Models
{
    public class Pedido : ViewModelBase, IAzureTabela
    {
        public string Id { get; set; }
        public string ClienteId { get; set; }
        public string FornecedorId { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataAgenda { get; set; }
        public DateTime DataEntrega { get; set; }

        [JsonIgnore, SQLite.Ignore]
        public string Entrega
        {
            get
            {
                if (DataEntrega == DateTime.MinValue)
                {
                    return string.Empty;
                }
                return DataEntrega.ToString();
            }
            set
            {
                DataEntrega = DateTime.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
            }
        }


        [JsonIgnore, SQLite.Ignore]
        public string NomeFornecedor { get; set; }

        [JsonIgnore, SQLite.Ignore]
        public string NomeCliente { get; set; }

        [JsonIgnore, SQLite.Ignore]
        public List<PedidoItens> listaItens { get; set; }

        private double totalPedido;
        [JsonIgnore]
        public double TotalPedido
        {
            get { return totalPedido; }
            set { SetProperty(ref totalPedido, value); }
        }
        private int totalItens;
        [JsonIgnore]
        public int TotalItens
        {
            get { return totalItens; }
            set { SetProperty(ref totalItens, value); }
        }


        //Metodo construtor
        public Pedido(string clienteId, string fornecedorId)
        {
            ClienteId = clienteId;
            FornecedorId = fornecedorId;
            DataEmissao = DateTime.Now;
            DataAgenda = DateTime.Now.AddHours(3);
            DataEntrega = DateTime.MinValue;
        }


        public event EventHandler DelegateAtualizadorLista;
        public void AtualizarLista()
        {
            DelegateAtualizadorLista(this, new EventArgs());
        }
    }
}
