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
    public partial class PessoaView : ContentPage
    {
        Base_Service client_cep = new Base_Service(Base_Service.URL_VIACEP);
        ReqRes_Service client_ReqRes_user = new ReqRes_Service("users");
        ReqRes_Service client_ReqRes_register = 
            new ReqRes_Service("register");
        
        //      Base_Service client_api = new Base_Service(Base_Service.URL_MINHAAPI);
        User_ReqRes md = new User_ReqRes();

        public PessoaView()
        {
            InitializeComponent();
        }

        private async void EtCep_Unfocused(object sender, FocusEventArgs e)
        {

            var cep_ret = await client_cep.Get<CEP>(etCep.Text);

            this.etCep.Text = cep_ret.cep;
            this.etLogradouro.Text = cep_ret.Logradouro;
            this.etComplemento.Text = cep_ret.Complemento;
            this.etBairro.Text = cep_ret.Bairro;
            this.etLocalidade.Text = cep_ret.Localidade;
            this.etUf.Text = cep_ret.Uf;

            //var pessoa_ret = await client_api.get<Pessoa>("");
            //this.etPessoaNome.Text = pessoa_ret.Nome;
            await client_ReqRes_user.Get<RetornoTeste>("2");

            md.name = cep_ret.Bairro;
            md.job = cep_ret.Localidade;

            var retornoPost = await client_ReqRes_user.Post(md);

            this.etUf.Text = $"{retornoPost.createdAt} {retornoPost.id}";

            await this.DisplayAlert("meu retorno",
                $"{retornoPost.createdAt} {retornoPost.id}",
                "Entendi");

            var usuarioEntrada = new Usuario
            {
                email="joao@senac.com.br",
                password = "123"
            };
            var usuarioSaida = await client_ReqRes_register
                .Post<Usuario, Usuario>(usuarioEntrada);

            await this.DisplayAlert("usuarioSaida",
                $"{usuarioSaida.token}",
                "Entendi");

        }
    }
}