using AppChamaGas.Models;
using AppChamaGas.Services.Azure;
using MonkeyCache.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppChamaGas.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PedidosView : ContentPage
	{
        PedidoAzureService pedido_Service = new PedidoAzureService();
        PedidoItensAzureService pedidoItens_Service = new PedidoItensAzureService();
        PessoaAzureService pessoa_Service = new PessoaAzureService();
        Pessoa usuarioLogado;
        public PedidosView ()
		{
			InitializeComponent ();
            usuarioLogado = Barrel.Current.Get<Pessoa>("pessoa");
		}


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            bool eh_distribuidor = usuarioLogado.Tipo == "Distribuidor";
            
            IEnumerable<Pedido> pedidos = await pedido_Service.ListarAsync();
            IEnumerable<PedidoItens> pedidosItens = await pedidoItens_Service.ListarAsync();
            IEnumerable<Pessoa> pessoas = await pessoa_Service.ListarAsync();
                
            if(eh_distribuidor)
                pedidos = pedidos.Where(p=>p.FornecedorId == usuarioLogado.Id);
            else
                pedidos = pedidos.Where(p => p.ClienteId == usuarioLogado.Id);

            foreach (var pedido in pedidos)
            {
                Pessoa pessoa = eh_distribuidor 
                                    ? pessoas.Where(p => p.Id == pedido.ClienteId).FirstOrDefault()
                                    : pessoas.Where(p => p.Id == pedido.FornecedorId).FirstOrDefault();                

                pedido.NomeFornecedor = pessoa.RazaoSocial;

                var itensFiltrados = pedidosItens.Where(i => i.PedidoId == pedido.Id).ToList();
                var total = itensFiltrados.Sum(i => i.ValorTotal);            }

            lvPedidos.ItemsSource = pedidos;

         

        }
    }
}