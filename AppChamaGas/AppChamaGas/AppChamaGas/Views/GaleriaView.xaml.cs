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
	public partial class GaleriaView : ContentPage
	{
		public GaleriaView ()
		{
			InitializeComponent ();
            ListarImagens();

        }

        private void ListarImagens()
        {
            //Url da imagem
            var uri = new Uri(@"https://picsum.photos/300/300");
            //Contador de 10 imagem
            for (int i = 0; i < 10; i++)
            {
                //Instancia de cada imagem
                var imagem = new Image
                {
                    Source = ImageSource.FromUri(uri),
                    //Aspect = Aspect.AspectFill
                };
                //Adiciona no layout
                flxLayout.Children.Add(imagem);
            }
        }
	}
}