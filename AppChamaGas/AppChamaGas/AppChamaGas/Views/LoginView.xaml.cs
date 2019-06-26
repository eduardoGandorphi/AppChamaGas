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
	public partial class LoginView : ContentPage
	{
        PessoaAzureService pessoaAzureService;
        UsuarioModel usuarioModel { get; set; }
		public LoginView ()
		{
			InitializeComponent ();
            pessoaAzureService = new PessoaAzureService();
            usuarioModel = new UsuarioModel();            
            this.BindingContext = usuarioModel;

		}

        private async void BtnEntrar_Clicked(object sender, EventArgs e)
        {
            //Verificação dos dados
            if (ValidarDados())
            {
                var pessoa = await pessoaAzureService.AutenticarUsuarioAsync(usuarioModel.Email, usuarioModel.Senha);
                //Verficação da autenticação do usuario
                if (pessoa != null)
                {
                    usuarioModel.Id = pessoa.Id;
                    usuarioModel.Email = pessoa.Email;
                    usuarioModel.Senha = pessoa.Senha;
                    usuarioModel.Permissao = pessoa.Tipo;
                    usuarioModel.Autenticado = true;
                    //Salvar banco de dados

                    //Define a nova mainpage principal
                    App.Current.MainPage = new MasterView();
                }
            }
            else
            {
                await DisplayAlert("Atenção", "E-mail ou senha inválidos", "Fechar");
            }
           
        }

        private void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new PessoaView());
        }

        private bool ValidarDados()
        {
            if (string.IsNullOrWhiteSpace(vEmail.Text) && 
                string.IsNullOrWhiteSpace(vSenha.Text))
            {
                vErro.IsVisible = true;
                vErro.Text = "Atenção, informe os seus dado corretamento";
                return false;
            }
            if (vSenha.Text.Length > 8)
            {
                vErro.Text = "Atenção, senha inválida (8 caracteres)";
                return false;
            }
            return true;
        }
    }
}