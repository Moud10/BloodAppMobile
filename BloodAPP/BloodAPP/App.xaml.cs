using BloodAPP.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodAPP
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new SignInPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
