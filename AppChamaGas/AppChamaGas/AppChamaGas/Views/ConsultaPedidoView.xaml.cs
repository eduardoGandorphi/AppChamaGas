using AppChamaGas.Models;
using AppChamaGas.Services.Azure;
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
    public partial class ConsultaPedidoView : ContentPage
    {
        PedidoAzureService pedido_service = new PedidoAzureService();
        Pedido pedido;
        public ConsultaPedidoView(Pedido pedido)
        {
            this.pedido = pedido;
            InitializeComponent();
            PopulaCampos(pedido);
        }
        private void PopulaCampos(Pedido pedido)
        {
            lblId.Text = pedido.Id;
            lblDataEmissao.Text = pedido.DataEmissao.ToString("dd/MM/yyyy");
            lblNomeForn.Text = pedido.NomeFornecedor;
            lblNomeCli.Text = pedido.NomeCliente;
            lvItens.ItemsSource = pedido.listaItens;
            lblTotal.Text = pedido.TotalPedido.ToString("C2");
            lblPrevisao.Text = pedido.DataAgenda.ToString("dd/MM/yyyy");

            if (pedido.DataEntrega == null || pedido.DataEntrega == DateTime.MinValue)            
                ControlaVisibilidadeEntrega(string.Empty);
            else
                ControlaVisibilidadeEntrega(pedido.DataEntrega.ToString("dd/MM/yyyy HH:mm"));              
        }

        private void ControlaVisibilidadeEntrega(string dataEntrega)
        {
            bool visibilidadeEntregaVazia = string.IsNullOrEmpty(dataEntrega);
            lblEntrega.Text = dataEntrega;
            lblLblEntrega.IsVisible = !visibilidadeEntregaVazia;
            lblEntrega.IsVisible = !visibilidadeEntregaVazia;

            btnEntregar.IsVisible = visibilidadeEntregaVazia;
        }

        private async void BtnEntregar_Clicked(object sender, EventArgs e)
        {
            this.pedido.DataEntrega = DateTime.Now;

            bool sucesso = await pedido_service.AlterarAsync(this.pedido);

            if (sucesso)
            {
                await Navigation.PopAsync();
                await this.DisplayAlert("Sucesso", "Data de entrega salva", "OK");                
            }
            else
                await this.DisplayAlert("Falha", "Falha ao alterar pedido.", "OK");
        }
    }
}