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
    public partial class UsuarioView : ContentPage
    {
        PessoaAzureService pessoaAzureServico;

        //Executa ao iniciar o formulario
        public UsuarioView()
        {
            InitializeComponent();
            pessoaAzureServico = new PessoaAzureService();
            ListarUsuariosAsync(vBusca.Text);
        }

        private async void ListarUsuariosAsync(string busca)
        {
            vCarregando.IsVisible = true;
            lvUsuarios.IsRefreshing = true;
            try
            {
                //Fez a consulta no banco de dados no serviço em nuvmem do Azure
                var usuarios = await pessoaAzureServico.ListarAsync();
                //Verifica se existe um texto para a busca
                if (!string.IsNullOrWhiteSpace(busca))
                {
                    usuarios
                        .Where(p =>
                            p.RazaoSocial.StartsWith(busca) ||
                            p.Endereco.Contains(busca) ||
                            p.Cep == busca)
                        .OrderBy(p => p.RazaoSocial);
                }

                //Popula a lista com o resultado da consulta
                lvUsuarios.ItemsSource = usuarios;
            }
            catch
            {
                await DisplayAlert("Atenção", "Não foi possivel realizar a consulta", "Fechar");
            }
            lvUsuarios.IsRefreshing = false;
            vCarregando.IsVisible = false;

        }

        //Executa ao recarregar o formulario
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ListarUsuariosAsync(vBusca.Text);
        }

        private void VBusca_SearchButtonPressed(object sender, EventArgs e)
        {
            //Faz a consulta pelo texto de busca
            ListarUsuariosAsync(vBusca.Text);
        }

        private void LvUsuarios_Refreshing(object sender, EventArgs e)
        {
            ListarUsuariosAsync(vBusca.Text);
        }
    }
}