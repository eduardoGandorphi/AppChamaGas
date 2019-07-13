using AppChamaGas.Interface;
using AppChamaGas.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppChamaGas.Models
{
    public class PedidoItens : ViewModelBase, IAzureTabela
    {
        //Propriedades
        public string Id { get; set; }
        public string PedidoId { get; set; }
        public string ProdutoId { get; set; }

        private int quantidade;
        public int Quantidade
        {
            get { return quantidade; }
            set { SetProperty(ref quantidade, value); }
        }
        public double Preco { get; set; }

        //Calculado internamente na classe
        private double valorTotal;
        public double ValorTotal
        {
            get { return valorTotal; }
            private set { SetProperty(ref valorTotal, value); }
        }

        [JsonIgnore]
        public Command MaisCommand { get; set; }
        [JsonIgnore]
        public Command MenosCommand { get; set; }
        [JsonIgnore]
        public string DescricaoProduto { get; set; }

        [JsonIgnore]
        public Pedido PedidoPai { get; set; }
        //Metodo construtor
        public PedidoItens(
            string pedidoId,
            string produtoId,
            string IdDoItem,
            int quantidade = 1,
            double preco = 0)
        {

            PedidoId = pedidoId;
            ProdutoId = produtoId;
            Id = IdDoItem;
            Quantidade = quantidade;
            Preco = preco;
            if (quantidade > 0 && preco > 0)
            {
                ValorTotal = Quantidade * Preco;
            }

            MaisCommand = new Command(Mais);
            MenosCommand = new Command(Menos);
        }
        private void Menos()
        {
            if (Quantidade > 0)
            {
                Quantidade -= 1;
                ValorTotal = Quantidade * Preco;


                PedidoPai.AtualizarLista();
            }
        }
        private void Mais()
        {
            Quantidade += 1;
            ValorTotal = Quantidade * Preco;

            PedidoPai.AtualizarLista();
        }
    }
}
