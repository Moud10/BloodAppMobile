using BloodAPP.Models;
using BloodAPP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodAPP.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LatestDonars : ContentPage
    {
        public ObservableCollection<BloodUser> BloodUsers;
        public LatestDonars()
        {
            InitializeComponent();
            BloodUsers = new ObservableCollection<BloodUser>();
            FindBloodDonars();
        }
        private async void FindBloodDonars()
        {
            try
            {
                ApiServices apiServices = new ApiServices();
                var bloodUsers = await apiServices.LatestBloodUser();
                foreach (var bloodUser in bloodUsers)
                {
                    BloodUsers.Add(bloodUser);
                }
                LvBloodDonars.ItemsSource = BloodUsers;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.Message + " Please turn on your network", "Cancel");
            }
        }

        private void LvBloodDonars_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedDonar = e.SelectedItem as BloodUser;
            if (selectedDonar != null)
            {
                Navigation.PushAsync(new DonarProfilePage(selectedDonar));
            }
            ((ListView)sender).SelectedItem = null;
        }
    }
}