﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppChamaGas.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AjudaView : ContentPage
	{
		public AjudaView ()
		{
			InitializeComponent ();
		}

        private void BtnVoltar_Clicked(object sender, EventArgs e)
        {
            //App.Current.MainPage = new MasterView();
            this.Navigation.PopModalAsync();
        }
    }
}