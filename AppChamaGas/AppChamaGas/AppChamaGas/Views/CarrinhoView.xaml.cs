using AppChamaGas.Models;
using AppChamaGas.Services.Azure;
using MonkeyCache.SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppChamaGas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarrinhoView : ContentPage
    {
        PedidoAzureService Pedido_Service = new PedidoAzureService();
        PedidoItensAzureService PedidoItens_Service = new PedidoItensAzureService();

        public static Pedido pedido = new Pedido("", "");

        public static ObservableCollection<PedidoItens> Itens =
            new ObservableCollection<PedidoItens>();

        Pessoa usuario;
        public CarrinhoView()
        {
            InitializeComponent();
            usuario = Barrel.Current.Get<Pessoa>("pessoa");
            CarrinhoView.pedido.ClienteId = usuario.Id;

            this.BindingContext = CarrinhoView.pedido;
            lvItens.ItemsSource = CarrinhoView.Itens;
            
        }

        private async void BtnSalvarPedido_Clicked(object sender, EventArgs e)
        {
            bool confirma = await DisplayAlert(
                "Confirmação",
                "Deseja realmente enviar esse pedido para nossa central  ?",
                "Sim",
                "Não");

            if (!confirma)
                return;

            if (!await Pedido_Service.IncluirAsync(CarrinhoView.pedido))
                return;

            var inserido = (await Pedido_Service.ListarAsync()).LastOrDefault();

            foreach (var item in CarrinhoView.Itens)
            {
                item.PedidoId = inserido.Id;
                item.Id = string.Empty;
                if (!await PedidoItens_Service.IncluirAsync(item))
                {
                    await this.DisplayAlert("Falha", "falha ao transmitir pedido", "ok");                    
                    return;
                }
            }
            ZeraPedido();
        }
        private async void ZeraPedido()
        {
            await Navigation.PopAsync();

            CarrinhoView.pedido.Id = string.Empty;
            CarrinhoView.pedido.FornecedorId = string.Empty;
            CarrinhoView.pedido.NomeFornecedor = string.Empty;
            CarrinhoView.pedido.TotalItens = 0;
            CarrinhoView.pedido.TotalPedido = 0;

            CarrinhoView.Itens.Clear();

            await this.DisplayAlert("Sucesso", "Sucesso ao transmitir pedido", "ok");
        }
    }
}