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
        IEnumerable<Pessoa> usuarios;

        //Executa ao iniciar o formulario
        public UsuarioView()
        {
            InitializeComponent();
            pessoaAzureServico = new PessoaAzureService();
            ListarUsuariosAsync(vBusca.Text);
        }

        private async void ListarUsuariosAsync(string busca=null)
        {            
            lvUsuarios.IsRefreshing = true;
            try
            {
                usuarios = null;
                //Fez a consulta no banco de dados no serviço em nuvmem do Azure
                usuarios = await pessoaAzureServico.ListarAsync();
                //Verifica se existe um texto para a busca
                if (!string.IsNullOrWhiteSpace(busca))
                {
                    lvUsuarios.ItemsSource = usuarios
                        .Where(p =>
                            p.RazaoSocial.Contains(busca))
                        .OrderBy(p => p.RazaoSocial)
                        .ToList();
                }
                else
                {
                    lvUsuarios.ItemsSource = usuarios
                        .OrderBy(p => p.RazaoSocial)
                        .ToList();
                }
            }
            catch
            {
                await DisplayAlert("Atenção", "Não foi possivel realizar a consulta", "Fechar");
            }
            lvUsuarios.IsRefreshing = false;        

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

        private void BtnAdicionar_Clicked(object sender, EventArgs e)
        {
            MasterView.NavegacaoMasterDetail.Detail.Navigation.PushAsync(new PessoaView());
        }

        private async void BtnRemover_Clicked(object sender, EventArgs e)
        {
            //Pega o valor do menu da lista CommandParameter
            string id = ((MenuItem)sender).CommandParameter.ToString();
            //Pega o item (objeto) da lista selecionado
            Pessoa usuario = usuarios.FirstOrDefault(p => p.Id == id);
            if (usuario != null)
            {
                //Pessoa usuario = lvUsuarios.SelectedItem as Pessoa;
                bool retorno = await pessoaAzureServico.ExcluirAsync(usuario);
                if (retorno)
                {
                    await DisplayAlert("Sucesso", "Registro excluido com sucesso", "Fechar");
                    ListarUsuariosAsync();
                    return;
                }
            }
            await DisplayAlert("Atenção", "Não foi possível a exclusão do registro ", "Fechar");                       
        }

        private void LvUsuarios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Pega o item selecionado "e" na lista
            Pessoa usuario = e.SelectedItem as Pessoa;
            //Vai para nova pagina (PessoaView) enviado o usuario
            MasterView.NavegacaoMasterDetail.Detail.Navigation.PushAsync(new PessoaView(usuario));

        }
    }
}