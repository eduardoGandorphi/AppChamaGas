using AppChamaGas.Models;
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

		public MenuView ()
		{
			InitializeComponent ();
            CarregarLista();

        }

        public void CarregarLista()
        {
            paginas = new List<Pagina>();
            paginas.Add(new Pagina
            {
                Titulo = "Pessoa",
                Icone = "",
                PaginaView = typeof(PessoaView)
            });
            paginas.Add(new Pagina
            {
                Titulo = "Login",
                Icone = "",
                PaginaView = typeof(LoginView)
            });
            paginas.Add(new Pagina
            {
                Titulo = "Pessoa",
                Icone = "",
                PaginaView = typeof(PessoaView)
            });

            lvMenu.ItemsSource = paginas;
        }

        private void LvMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Seleciona a pagina
            var pagina = e.SelectedItem as Pagina;
            //Verifica se existe a pagina
            if (pagina != null)
            {
                //Inicia a navegacao
                MasterView.NavegacaoMasterDetail.IsPresented = false;
                MasterView.NavegacaoMasterDetail.Detail.Navigation.PopToRootAsync();
                
                //Cria a pagina view 
                Page paginaView = Activator.CreateInstance(pagina.PaginaView) as Page;
                //Navega para a pagina view
                MasterView.NavegacaoMasterDetail.Detail.Navigation.PushAsync(paginaView);
                //Desativa o item selecionado
                lvMenu.SelectedItem = null;
            }

        }
    }
}