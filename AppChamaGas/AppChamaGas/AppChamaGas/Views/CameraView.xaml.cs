using AppChamaGas.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppChamaGas.Extensions;
using AppChamaGas.ViewModel;

namespace AppChamaGas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraView : ContentPage
    {
        public CameraView()
        {
            InitializeComponent();
            this.BindingContext = new CameraViewModel();
        }

        //private async void btnTiraFoto_Clicked(object sender, EventArgs e)
        //{            
        //    var foto_md = await Photo.TiraFoto("nome2.jpg");

        //    imgBanco.Source   = foto_md.fotoArray.ToImageSource();
        //    imgGaleria.Source = foto_md.PathGaleria;
        //    imgInterna.Source = foto_md.PathInterno;
        //}
    }
}