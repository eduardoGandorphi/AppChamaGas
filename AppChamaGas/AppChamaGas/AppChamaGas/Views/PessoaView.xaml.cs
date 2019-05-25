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
        ReqRes_Service client_ReqRes = new ReqRes_Service("users");
        //      Base_Service client_api = new Base_Service(Base_Service.URL_MINHAAPI);
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
            await client_ReqRes.Get<RetornoTeste>("2");
        }
    }
}