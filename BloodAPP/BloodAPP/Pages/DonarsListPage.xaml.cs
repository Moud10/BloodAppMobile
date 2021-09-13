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
    public partial class DonarsListPage : ContentPage
    {
        public DonarsListPage(string country, string bloodType)
        {
            InitializeComponent();
            var apiServices = new ApiServices();
            maListeView.ItemsSource = apiServices.FindBlood(country,bloodType);
        }
    }
}