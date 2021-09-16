using BloodAPP.Models;
using BloodAPP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodAPP.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DonarsListPage : ContentPage
    {
        public ObservableCollection<BloodUser> BloodUsers;
        private string _bloodGroup;
        private string _country;
        public DonarsListPage(string country, string bloodType)
        {
            InitializeComponent();
            BloodUsers = new ObservableCollection<BloodUser>();
            _bloodGroup = bloodType;
            _country = country;
            FindBloodDonars();
        }
        private async void FindBloodDonars()
        {
            try
            {
                ApiServices apiServices = new ApiServices();
                var bloodUsers = await apiServices.FindBlood(_country, _bloodGroup);
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
            BloodUser selectedDonar = e.SelectedItem as BloodUser;
            if (selectedDonar!=null)
            {
                Navigation.PushAsync(new DonarProfilePage(selectedDonar));
            }
            ((ListView) sender).SelectedItem=null;
        }
    }
}