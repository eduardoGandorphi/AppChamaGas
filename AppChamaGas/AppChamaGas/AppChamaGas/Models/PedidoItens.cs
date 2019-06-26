using AppChamaGas.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppChamaGas.Models
{
    public class PedidoItens : IAzureTabela
    {
        //Propriedades
        public string Id { get; set; }
        public string PedidoId { get; set; }
        public string ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public double Preco { get; set; }
        //Calculado internamente na classe
        public double ValorTotal { get; private set; }

        //Metodo construtor
        public PedidoItens(
            string pedidoId,
            string produtoId,
            int quantidade = 1,
            double preco = 0)
        {
            PedidoId = pedidoId;
            ProdutoId = produtoId;
            Quantidade = quantidade;
            Preco = preco;
            if (quantidade > 0 && preco > 0)
            {
                ValorTotal = Quantidade * Preco;
            }
        }
    }
}
