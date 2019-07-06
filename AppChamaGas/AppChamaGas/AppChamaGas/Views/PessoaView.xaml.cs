using AppChamaGas.Extensions;
using AppChamaGas.Helpers;
using AppChamaGas.Models;
using AppChamaGas.Services;
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
    public partial class PessoaView : ContentPage
    {
        //Servicos do Azure
        PessoaAzureService pessoaAzureServico = new PessoaAzureService();
        Pessoa pessoa;
        Base_Service client_cep = new Base_Service(Base_Service.URL_VIACEP);
        ReqRes_Service client_ReqRes_user = new ReqRes_Service("users");
        ReqRes_Service client_ReqRes_register =
            new ReqRes_Service("register");

        //      Base_Service client_api = new Base_Service(Base_Service.URL_MINHAAPI);
        User_ReqRes md = new User_ReqRes();

        Pessoa PessoaBC { get { return (Pessoa)BindingContext; } }
        //Metodo construtor vai receber como parametro uma pessoa
        public PessoaView(Pessoa usuario = null)
        {
            InitializeComponent();
            ListarTipo();

            if (usuario == null || string.IsNullOrEmpty(usuario.Id))
                usuario = Barrel.Current.Get<Pessoa>("pessoa");

            if (usuario == null)
                usuario = new Pessoa();

            pessoa = usuario;
            this.BindingContext = pessoa;

            pessoa.FotoSource = pessoa.FotoByte.ToImageSource();
            imgFoto.Source = pessoa.FotoSource;

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
                email = "Xeve.holt@reqres.in",
                password = "pistol"
            };


            try
            {
                var usuarioSaida = await client_ReqRes_register
                .Post<Usuario, Usuario>(usuarioEntrada);

                await this.DisplayAlert("usuarioSaida",
                    $"{usuarioSaida.token}",
                    "Entendi");

                var usuarioPut = await client_ReqRes_register
               .Post<Usuario, Usuario>(usuarioEntrada);

                await this.DisplayAlert("usuarioSaida",
                    $"{usuarioPut.token}",
                    "Entendi");
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Erro", ex.Message, "OK");
            }

        }

        private async void BtnSalvar_Clicked(object sender, EventArgs e)
        {
            vCarregando.IsVisible = true;
            vCarregando.IsRunning = true;
            var resultado = await SalvarAsync();
            if (resultado)
            {
                await DisplayAlert("Confirma", "Registro salvo com sucesso", "Fechar");
                await MasterView.NavegacaoMasterDetail.Detail.Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Atenção", "Não foi possivel salvar o registro", "Fechar");
            }
            vCarregando.IsVisible = false;
            vCarregando.IsRunning = false;
        }

        private async Task<bool> SalvarAsync()
        {
            pessoa = new Pessoa();
            pessoa.Id = lblId.Text;
            pessoa.RazaoSocial = etRazaoSocial.Text;
            pessoa.Tipo = picTipo.SelectedItem.ToString();
            pessoa.Endereco = etLogradouro.Text;
            pessoa.Numero = etNumero.Text;
            pessoa.Bairro = etBairro.Text;
            pessoa.Cep = etCep.Text;
            pessoa.Cidade = etLocalidade.Text;
            pessoa.Uf = etUf.Text;
            pessoa.Telefone = etTelefone.Text;
            pessoa.Email = etEmail.Text;
            pessoa.Senha = etSenha.Text;

            var pessoa_Bindada = ((Pessoa)this.BindingContext);
            pessoa.FotoByte = pessoa_Bindada.FotoByte;

            if (string.IsNullOrWhiteSpace(pessoa.Id))
            {
                return await pessoaAzureServico.IncluirAsync(pessoa);
            }
            else
            {
                Barrel.Current.Empty("pessoa");
                Barrel.Current.Add("pessoa", pessoa, TimeSpan.FromMinutes(1));
                return await pessoaAzureServico.AlterarAsync(pessoa);
            }
        }

        private void ListarTipo()
        {
            List<string> tipos = new List<string>();
            tipos.Add("Consumidor");
            tipos.Add("Distribuidor");
            picTipo.ItemsSource = tipos;
            picTipo.SelectedIndex = 0;
        }

        private async void BtnFoto_Clicked(object sender, EventArgs e)
        {

            Foto_MD md = await Photo.TiraFoto();

            if (md == null)
                return;

            this.imgFoto.Source = md.fotoArray.ToImageSource();
            PessoaBC.FotoByte = md.fotoArray;
            
        }
    }
}