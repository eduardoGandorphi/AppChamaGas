using AppChamaGas.Extensions;
using AppChamaGas.Helpers;
using AppChamaGas.Models;
using AppChamaGas.Services.Azure;
using MonkeyCache.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppChamaGas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProdutosView : ContentPage
    {
        PessoaAzureService pessoa_Service = new PessoaAzureService();
        ProdutoAzureService produto_Service = new ProdutoAzureService();

        Pessoa usuarioLogado;
        bool eh_Distribuidor;
        public ProdutosView()
        {
            InitializeComponent();

            usuarioLogado = Barrel.Current.Get<Pessoa>("pessoa");


        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            eh_Distribuidor = usuarioLogado.Tipo == "Distribuidor";

            if (eh_Distribuidor)
                AdicionaBotaoNovoProduto();

            lblTitulo.Text = eh_Distribuidor ? "Meus Produtos" : "Lista de Produtos";

            IEnumerable<Pessoa> pessoas = await pessoa_Service.ListarAsync();
            IEnumerable<Produto> produtos = await produto_Service.ListarAsync();

            if (eh_Distribuidor)
            {
                pessoas = pessoas.Where(p => p.Id == usuarioLogado.Id).ToList();
                produtos = produtos.Where(p => p.FornecedorId == usuarioLogado.Id).ToList();
            }
            else
                pessoas = pessoas.Where(p => p.Tipo == "Distribuidor").ToList();

            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var mPosition = await Geolocation.GetLocationAsync(request);

            foreach (Produto produto in produtos)
            {
                var forn = pessoas.Where(p => p.Id == produto.FornecedorId).FirstOrDefault();

                if (forn == null)
                    continue;

                produto.FornecedorNome = forn.RazaoSocial;
                produto.Latitude = forn.Latitude;
                produto.Longitude = forn.Longitude;

                Location locForn = new Location(forn.Latitude, forn.Longitude);
                forn.Distancia =
                    mPosition.CalculateDistance(locForn, DistanceUnits.Kilometers);

                produto.Distancia = $"{forn.Distancia.ToString("N4")} KMs";

                produto.FotoSource = produto.FotoByte.ToImageSource();
            }

            lvProdutos.ItemsSource = produtos;

            //lvProdutos.ItemsSource = new List<Produto>
            //{
            //    new Produto
            //    {
            //        Descricao ="Gás do bom",
            //        Distancia = "5km",
            //        FornecedorNome = "zé do gás",
            //        Preco = 10.00,                  
            //    },
            //    new Produto
            //    {
            //        Descricao ="Gás do bom",
            //        Distancia = "5km",
            //        FornecedorNome = "zé do gás",
            //        Preco = 10.00,
            //    },
            //    new Produto
            //    {
            //        Descricao ="Gás do bom",
            //        Distancia = "5km",
            //        FornecedorNome = "zé do gás",
            //        Preco = 10.00,
            //    },
            //};
        }

        private void AdicionaBotaoNovoProduto()
        {
            if (this.ToolbarItems.Count == 0)
                this.ToolbarItems.Add(new ToolbarItem
                {
                    Text = "Add",
                    Command = new Command(AbrirTelaCadastroProduto),
                });
        }

        private void AbrirTelaCadastroProduto()
        {
            Navigation.PushAsync(new ProdutoView());
        }
    }
}