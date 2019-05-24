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
	public partial class MasterView : MasterDetailPage
	{
        //Navegacao para estrutura master detail page
        public static MasterDetailPage NavegacaoMasterDetail { get; set; }

        public MasterView ()
		{
			InitializeComponent ();
            //Configuracao
            //Principal
            this.Detail = new NavigationPage(new HomeView()
            {
                Title = "ChamaGas",
                Icon =""
            });
            //Menu
            this.Master = new MenuView()
            {
                Title = "Menu"
            };
            this.MasterBehavior = MasterBehavior.Popover;

            //Inicializa a navegacao master detail page
            NavegacaoMasterDetail = this;
		}
	}
}