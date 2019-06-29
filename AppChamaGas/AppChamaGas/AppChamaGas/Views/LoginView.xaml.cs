using AppChamaGas.Models;
using AppChamaGas.Services.Azure;
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
	public partial class LoginView : ContentPage
	{
        PessoaAzureService pessoaAzureService;
        UsuarioModel usuarioContext { get; set; }
		public LoginView ()
		{
			InitializeComponent ();
            pessoaAzureService = new PessoaAzureService();
            usuarioContext = new UsuarioModel()
            {
                Email ="teste@teste.com.br",
                Senha = "12345678",
            };           
            this.BindingContext = usuarioContext;
		}

        private async void BtnEntrar_Clicked(object sender, EventArgs e)
        {
            BloquearTela(true);
            //Verificação dos dados
            if (ValidarDados())
            {
                var pessoa = await pessoaAzureService.AutenticarUsuarioAsync(usuarioContext.Email, usuarioContext.Senha);
                //Verficação da autenticação do usuario
                if (pessoa != null)
                {
                    //usuarioContext.Id = pessoa.Id;
                    //usuarioContext.Email = pessoa.Email;
                    //usuarioContext.Senha = pessoa.Senha;
                    //usuarioContext.Permissao = pessoa.Tipo;
                    //usuarioContext.Autenticado = true;
                    //Cache de dados para a pessoa
                    //Salva os dados da pessoa
                    Barrel.Current.Add(key: "pessoa", data: pessoa, expireIn: TimeSpan.FromMinutes(1));
                    

                    //Define a nova mainpage principal
                    App.Current.MainPage = new MasterView();
                    BloquearTela(false);
                }
                else
                {
                    BloquearTela(false);
                    await DisplayAlert("Atenção", "Conta não encontrado", "Fechar");
                }
            }
            else
            {
                BloquearTela(false);
                await DisplayAlert("Atenção", "E-mail ou senha inválidos", "Fechar");                
            }
            return;
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
            else if (vSenha.Text.Length < 8)
            {
                vErro.Text = "Atenção, senha inválida (8 caracteres)";
                return false;
            }
            vErro.Text = string.Empty;
            return true;
        }

        private void BloquearTela(bool resultado)
        {
            //Default
            aiCarregar.IsVisible = true;
            aiCarregar.IsRunning = true;
            vEmail.IsEnabled = false;
            vSenha.IsEnabled = false;            
            btnEntrar.IsEnabled = false;
            btnRegistrar.IsEnabled = false;
            //Verificacao do resultado
            if (!resultado)
            {
                aiCarregar.IsVisible = false;
                aiCarregar.IsRunning = false;
                vEmail.IsEnabled = true;
                vSenha.IsEnabled = true;
                btnEntrar.IsEnabled = true;
                btnRegistrar.IsEnabled = true;
            }
        }

        private void VerificarLogin()
        {
            var usuarioLogado = Barrel.Current.Get<Pessoa>("pessoa");
            if (usuarioLogado != null)
            {
                App.Current.MainPage = new MasterView();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            VerificarLogin();
        }
    }
}