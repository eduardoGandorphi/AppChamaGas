using AppChamaGas.Helpers;
using AppChamaGas.Interface;
using AppChamaGas.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppChamaGas.Models
{
    public class Pessoa : ViewModelBase, IAzureTabela
    {
        public string Id { get; set; }
        public string Tipo { get; set; }
        public string RazaoSocial { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }

        private string foto;
        public string Foto
        {
            get { return foto; }
            set { SetProperty(ref foto, value); }
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool Deleted { get; set; }

        [JsonIgnore]//ignora campo quando converte para texto no formato json.
        public double Distancia { get; set; }

        private bool botaoVisivel;
        [JsonIgnore]
        public bool BotaoVisivel
        {
            get { return botaoVisivel; }
            set { SetProperty(ref botaoVisivel , value); }
        }

        private bool imageVisivel;
        [JsonIgnore]
        public bool ImageVisivel
        {
            get { return imageVisivel; }
            set { SetProperty(ref imageVisivel, value); }
        }

        [JsonIgnore]
        public Command TiraFotoCommand { get; set; }

        [JsonIgnore]
        public string TextoBotaoFoto { get; set; }
        public Pessoa()
        {
            TiraFotoCommand = new Command(TiraFoto);

            BotaoVisivel = true;
            ImageVisivel = false;

            TextoBotaoFoto = Font_Index.camera;
        }

        public async void TiraFoto()
        {
            Foto_MD md = await Photo.TiraFoto();

            BotaoVisivel = false;
            ImageVisivel = true;

            this.Foto = md.PathGaleria;

        }


    }
}
