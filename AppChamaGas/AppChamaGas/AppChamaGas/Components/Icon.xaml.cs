using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppChamaGas.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Icon : Label
    {
        public Icon()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.Android)
                FontFamily = "Font Awesome 5 Free-Solid-900.otf#Font Awesome 5 Free Solid";
            
                    //FontFamily = "fa-solid-900.ttf#Font Awesome 5 Free Solid";
            if (Device.RuntimePlatform == Device.iOS)
                FontFamily = "Font Awesome 5 Free";

            if (Device.RuntimePlatform == Device.UWP)
                FontFamily = "Assets/fa-solid-900.ttf#Font Awesome 5 Free Solid";

        }
    }

}