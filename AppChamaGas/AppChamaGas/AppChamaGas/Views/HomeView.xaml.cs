using AppChamaGas.Helpers;
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
	public partial class HomeView : ContentPage
	{
		public HomeView ()
        {
            InitializeComponent();

        }

        private async Task tiraFoto()
        {
            var foto = await Foto.ChamaCamera();
            mImage.Source = foto;
        }
    }
}