﻿using AppChamaGas.Extensions;
using AppChamaGas.Helpers;
using AppChamaGas.Models;
using AppChamaGas.Services.Azure;
using MonkeyCache.SQLite;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        private void PopuleBindings()
        {
            CarrinhoView.pedido.DelegateAtualizadorLista += ColecaoAlterada;
            this.BindingContext = CarrinhoView.pedido;
            CarrinhoView.Itens.CollectionChanged += ColecaoAlterada;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (string.IsNullOrEmpty(CarrinhoView.pedido.FornecedorId))
                PopuleBindings();

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

        private void LvProdutos_ItemTapped(object sender, ItemTappedEventArgs args)
        {            
            var prd = (Produto)args.Item;

            string proximoId = (CarrinhoView.Itens.Count() + 1).ToString();

            var it = new PedidoItens("", prd.Id, proximoId, 1, prd.Preco)
            { DescricaoProduto = prd.Descricao,
              PedidoPai = CarrinhoView.pedido};

            
            CarrinhoView.Itens.Add(it);
            CarrinhoView.pedido.FornecedorId = prd.FornecedorId;
        }

        private void ColecaoAlterada(object sender, EventArgs e)
        {
            CarrinhoView.pedido.TotalPedido =  CarrinhoView.Itens.Sum(p => p.ValorTotal);
            CarrinhoView.pedido.TotalItens = CarrinhoView.Itens.Count();            
        }

        private void stkCarrinho_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CarrinhoView());
        }

        private void EtBusca_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.DisplayAlert("msg", "TEXTO ALTERADO", "ok");
        }

        private void EtBusca_Unfocused(object sender, FocusEventArgs e)
        {
            this.DisplayAlert("msg", "TEXTO DESFOCADO", "ok");
        }

        private void LvProdutos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            lvProdutos.SelectedItem = null;
        }
    }
}