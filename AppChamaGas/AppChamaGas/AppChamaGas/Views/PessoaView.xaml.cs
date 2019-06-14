using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Linq;

namespace AppChamaGas.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PessoaView : ContentPage
	{
		public PessoaView ()
		{
			InitializeComponent ();
            IsBusy = true;
            GetInfo();
		}

        private async void GetInfo()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                var myLocation = await Geolocation.GetLastKnownLocationAsync();

                var SENAC = new Location(-20.814622, -49.377225);
                var mc = new Location(-20.822608, -49.384599);
                var dist = location.CalculateDistance(SENAC, DistanceUnits.Kilometers);
                //var loc = Xamarin.Essentials.Geocoding.GetLocationsAsync();
                var places = await Xamarin.Essentials.Geocoding.GetPlacemarksAsync(mc.Latitude, mc.Longitude);

                var p = places.FirstOrDefault();
                Device.BeginInvokeOnMainThread(() =>
                    {
                        this.IsBusy = false;
                    this.DisplayAlert("ok",$"{p.PostalCode} {p.CountryName} {p.FeatureName} {p.Locality}" ,"ok");
                    });
            }
            catch (Exception e)
            {

              Device.BeginInvokeOnMainThread(() => { this.DisplayAlert("erro",e.Message,"ok"); });
            }
            
        }

        
    }
}