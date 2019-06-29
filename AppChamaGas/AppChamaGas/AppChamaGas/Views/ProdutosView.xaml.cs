using AppChamaGas.Models;
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
	public partial class ProdutosView : ContentPage
	{
        Pessoa usuarioLogado;
        bool eh_Distribuidor;
		public ProdutosView ()
		{
			InitializeComponent ();

            usuarioLogado = Barrel.Current.Get<Pessoa>("pessoa");            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            eh_Distribuidor = usuarioLogado.Tipo == "Distribuidor";

            lblTitulo.Text = eh_Distribuidor ? "Meus Produtos" : "Lista de Produtos";

            //busca produtos.

            lvProdutos.ItemsSource = new List<Produto>
            {
                new Produto
                {
                    Descricao ="Gás do bom",
                    Distancia = "5km",
                    FornecedorNome = "zé do gás",
                    Preco = 10.00,                  
                },
                new Produto
                {
                    Descricao ="Gás do bom",
                    Distancia = "5km",
                    FornecedorNome = "zé do gás",
                    Preco = 10.00,
                },
                new Produto
                {
                    Descricao ="Gás do bom",
                    Distancia = "5km",
                    FornecedorNome = "zé do gás",
                    Preco = 10.00,
                },
            };
        }
    }
}