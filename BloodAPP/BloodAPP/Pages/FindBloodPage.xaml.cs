﻿using BloodAPP.Services;
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
    public partial class FindBloodPage : ContentPage
    {
        public FindBloodPage()
        {
            InitializeComponent();
        }

        private void BtnSearch_Clicked(object sender, EventArgs e)
        {
            var country = PickerCountry.Items[PickerCountry.SelectedIndex];
            var BloodGroup = PickerBloodGroup.Items[PickerBloodGroup.SelectedIndex].Trim().Replace("+","%2B");
            Navigation.PushAsync(new DonarsListPage(country, BloodGroup));
        }
    }
}