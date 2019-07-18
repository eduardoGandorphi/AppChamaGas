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
        ProdutoAzureService produto_Service = new ProdutoAzureService();
        Pessoa usuarioLogado;
        IEnumerable<Produto> listaProdutos;
        public PedidosView ()
		{
			InitializeComponent ();
            usuarioLogado = Barrel.Current.Get<Pessoa>("pessoa");

            this.Appearing += CarregaProdutos;
		}
        private async void CarregaProdutos(object sender, EventArgs e)
        {
            listaProdutos = await produto_Service.ListarAsync();
        }
        private void LvPedidos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var ped = (Pedido)e.Item;

            if (listaProdutos == null)
            {
                DisplayAlert("Alerta", "Estamos carregando tudo para você. Tente Novamente.", "Ok");
                return;
            }

            foreach (var item in ped.listaItens)
                item.DescricaoProduto = listaProdutos.Where(p => p.Id == item.ProdutoId)
                                            .FirstOrDefault().Descricao;

            Navigation.PushAsync(new ConsultaPedidoView(ped));
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
                pedido.NomeFornecedor = pessoas.Where(p => p.Id == pedido.FornecedorId)
                    .FirstOrDefault().RazaoSocial;

                pedido.NomeCliente = pessoas.Where(p => p.Id == pedido.ClienteId)
                    .FirstOrDefault().RazaoSocial;

                var itensFiltrados = pedidosItens.Where(i => i.PedidoId == pedido.Id).ToList();
                pedido.listaItens = itensFiltrados;
                var total = itensFiltrados.Sum(i => i.ValorTotal);            }

            lvPedidos.ItemsSource = pedidos;


        }


    }
}