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
	public partial class LoginView : ContentPage
	{
        Usuario usuario { get; set; }
		public LoginView ()
		{
			InitializeComponent ();
            usuario = new Usuario();
            usuario.email = "teste@teste.com.br";
            this.BindingContext = usuario;

		}

        private void BtnEntrar_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Informações", $"E-mail:{ usuario.email } Senha { usuario.Senha }", "Fechar");
        }
    }
}