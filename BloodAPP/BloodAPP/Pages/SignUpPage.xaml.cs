using BloodAPP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodAPP.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            try
            {
                var apiServices = new ApiServices();
                bool response = await apiServices.RegisterUser(EntEmail.Text, EntPassword.Text, EntConfirmPassword.Text);
                if (!response)
                {
                    await DisplayAlert("Alert", "Something wrong...", "Cancel");
                }
                else
                {
                    await DisplayAlert("Hello", "Your account has been created", "Ok");
                    await Navigation.PopToRootAsync();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.Message + " Please turn on your network", "Cancel");
            }
        }
    }
}