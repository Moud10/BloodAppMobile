using BloodAPP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodAPP.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this,false);
        }
        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                var apiServices = new ApiServices();
                bool response = await apiServices.LoginUser(entEmail.Text, entPassword.Text);
                if (!response)
                {
                    await DisplayAlert("Alert", "Something wrong...", "Cancel");
                }
                else
                {
                    Navigation.InsertPageBefore(new HomePage(), this);
                    await Navigation.PopAsync();
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Alert", ex.Message + " Please turn on your network", "Cancel");
            }
        }
        private void TapSignUp_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }
    }
}