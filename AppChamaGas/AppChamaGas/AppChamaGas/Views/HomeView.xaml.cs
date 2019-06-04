﻿using AppChamaGas.Models;
using AppChamaGas.Services.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

namespace AppChamaGas.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomeView : ContentPage
	{
        PessoaAzureService pessoa_service = new PessoaAzureService();
		public HomeView ()
		{
			InitializeComponent ();
		}

        protected async override void OnAppearing()
        {
            var mPosition = new Location(-20.8141467, -49.3758587);

            List<Pessoa> fornecedores = (List<Pessoa>)
                await pessoa_service.List(etBusca.Text);

            foreach (var forn in fornecedores)
            {
                Location locForn = new Location(forn.Latitude, forn.Longitude);
                forn.Distancia =
                    mPosition.CalculateDistance(locForn, DistanceUnits.Kilometers);
            }
            var fornOrdenado = fornecedores.OrderBy(f => f.Distancia).ToList();

            lvForns.ItemsSource = fornOrdenado;
            base.OnAppearing();
        }
    }
}