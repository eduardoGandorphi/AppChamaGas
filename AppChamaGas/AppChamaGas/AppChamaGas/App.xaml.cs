using AppChamaGas.Helpers;
using AppChamaGas.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppChamaGas
{
    public partial class App : Application
    {
        public static string uriAzure = "https://chamagaspdm.azurewebsites.net";

        public App()
        {
            InitializeComponent();
            Conexao.Initialize();
            //Habilita a pagina principal
            //MainPage = new CameraView();//new MasterView();
            //MainPage = new MasterView();
            MainPage = new LoginView();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
