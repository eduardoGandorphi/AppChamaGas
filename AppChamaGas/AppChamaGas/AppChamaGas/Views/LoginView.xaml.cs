using AppChamaGas.Helpers;
using AppChamaGas.Models;
using AppChamaGas.Services;
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
            usuario.Email = "15014040";
            this.BindingContext = usuario;

        }

       
        private async void BtnEntrar_Clicked(object sender, EventArgs args)
        {

            tiraFoto();
            //DisplayAlert("Informações", $"E-mail:{ usuario.Email } Senha { usuario.Senha }", "Fechar");
            //var serv = new Base_Services();
            //try
            //{
            //    //var cep_md = await serv.Get<Cep_MD>(usuario.Email);
            //    //await DisplayAlert("Cep", $"{cep_md.Cep}\n{cep_md.Logradouro}\n{cep_md.Complemento}\n{cep_md.Bairro}\n{cep_md.Uf}\n{cep_md.Unidade}\n{cep_md.Ibge}\n{cep_md.Gia}", "OK");

            //    var User_MD = await serv.Put<User_MD>(new User_MD { Name = "joao", Job="programmer"});
            //    await DisplayAlert("retorno", $"{User_MD.Id}\n{User_MD.Name}\n{User_MD.Job}\n{User_MD.CreatedAt.ToString("dd/MM/yyyy")}\n{User_MD.UpdatedAt.ToString("dd/MM/yyyy")}", "OK");
            //}
            //catch (Exception e)
            //{
            //    await DisplayAlert("Erro",e.Message,"OK");
            //}
            
        }


        private async Task tiraFoto()
        {
            var foto = await Foto.ChamaCamera();
            mImage.Source = foto;
        }
    }
}