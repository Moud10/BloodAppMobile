using BloodAPP.Models;
using BloodAPP.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodAPP.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void Tblogout_Clicked(object sender, EventArgs e)
        {
            Settings.AccessToken = "";
            Settings.UserName = "";
            Settings.Password = "";
            Navigation.InsertPageBefore(new SignInPage(),this);
            Navigation.PopAsync();
        }

        private void TapFindBlood_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FindBloodPage());
        }

        private void TapRegisterBlood_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterBloodPage());
        }

        private void TapLatestDonars_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LatestDonars());
        }

        private void TapHelp_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HelpPage());
        }
    }
}