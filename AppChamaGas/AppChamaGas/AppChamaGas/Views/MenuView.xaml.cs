using AppChamaGas.Extensions;
using AppChamaGas.Helpers;
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
	public partial class MenuView : ContentPage
	{
        List<Pagina> paginas;
        //Pessoa pessoaContext;

		public MenuView ()
		{
			InitializeComponent ();
            //var pessoa = Barrel.Current.Get<Pessoa>("pessoa");
            //if (pessoa != null)
            //    pessoaContext = pessoa;
            ExibirPessoa();
            CarregarLista();

        }

        public void CarregarLista()
        {
            paginas = new List<Pagina>();
            paginas.Add(new Pagina
            {
                Titulo = "Perfil",
                Icone = Font_Index.user,
                PaginaView = typeof(PessoaView)
            });
            paginas.Add(new Pagina
            {
                Titulo = "Produtos",
                Icone = Font_Index.box,
                PaginaView = typeof(ProdutosView)
            });
            paginas.Add(new Pagina
            {
                Titulo = "Pedidos",
                Icone = Font_Index.list,
                PaginaView = typeof(PedidosView)
            });
            paginas.Add(new Pagina
            {
                Titulo = "Galeria",
                Icone = Font_Index.list,
                PaginaView = typeof(GaleriaView)
            });
            //paginas.Add(new Pagina
            //{
            //    Titulo = "Ajuda",
            //    Icone = Font_Index.list,
            //    PaginaView = typeof(IntroducaoView)
            //});
            /*
            paginas.Add(new Pagina
            {
                Titulo = "Login",
                Icone = Font_Index.key,
                PaginaView = typeof(LoginView)
            });
            paginas.Add(new Pagina
            {
                Titulo = "Usuarios",
                Icone = Font_Index.users,
                PaginaView = typeof(UsuarioView)
            });
            */

            lvMenu.ItemsSource = paginas;
        }

        private void LvMenu_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            foreach (Pagina item in lvMenu.ItemsSource)            
                item.CorLetra = Color.Gray;
            
            //Seleciona a pagina
            var pagina = e.Item as Pagina;
            //Verifica se existe a pagina
            if (pagina != null)
            {
                pagina.CorLetra = Color.Black;
                //Inicia a navegacao
                MasterView.NavegacaoMasterDetail.IsPresented = false;
                //MasterView.NavegacaoMasterDetail.Detail.Navigation.PopToRootAsync();

                Page paginaView = null;
                //Cria a pagina view 
                if (pagina.PaginaView == typeof(PessoaView))
                    paginaView = new PessoaView(new Pessoa());
                else if (pagina.PaginaView == typeof(CameraView))
                    paginaView = new CameraView();
                else
                    paginaView = Activator.CreateInstance(pagina.PaginaView) as Page;

                //Navega para a pagina view
                //MasterView.NavegacaoMasterDetail.Detail.Navigation.PushAsync(paginaView);
                MasterView.NavegacaoMasterDetail.Detail = new NavigationPage(paginaView);
                //Desativa o item selecionado
                lvMenu.SelectedItem = null;
            }

        }

        private void ExibirPessoa()
        {
            var pessoa = Barrel.Current.Get<Pessoa>("pessoa");
            if (pessoa != null)
            {
                vNome.Text = pessoa.RazaoSocial;
                vEmail.Text = pessoa.Email;
                pessoa.FotoSource = pessoa.FotoByte.ToImageSource();
                vFoto.Source = pessoa.FotoSource;
                    //vFoto.Source = ImageSource.FromStream(() =>                    
                //);
            }
        }

        private void GesSair_Tapped(object sender, EventArgs e)
        {
            //Finaliza a secao do usuario
            Barrel.Current.Empty("pessoa");
            //Fechar o aplicativo
            //App.Current.Quit();
            App.Current.MainPage = new LoginView();
        }

        private void GesExibir_Tapped(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new AjudaView());
        }
    }
}