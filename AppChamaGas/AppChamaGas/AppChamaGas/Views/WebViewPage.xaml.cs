using AppChamaGas.Helpers;
using AppChamaGas.Models;
using Plugin.Share;
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
	public partial class WebViewPage : ContentPage
	{
        string html;
		public WebViewPage (Pedido pedido, IEnumerable<PedidoItens> itens)
		{
			InitializeComponent ();

            this.html = GeradorRelatorioPedido.GerarHtml(pedido, itens);
            wvPrincipal.Source = new HtmlWebViewSource { Html = this.html };
        }
        public string SalvarArquivo()
        {
            return FileManager.Save(this.html);
        }       
        public void btnShare_Clicked(object sender, EventArgs args)
        {
            var caminho = SalvarArquivo();
            //CrossShare.Current.ShareLink("https://www.google.com");
        }
	}
}