using BloodAPP.Helpers;
using BloodAPP.Models;
using BloodAPP.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class RegisterBloodPage : ContentPage
    {
        public MediaFile file;
        public RegisterBloodPage()
        {
            InitializeComponent();
        }

        private async void TapOpenCamera_Tapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });
            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");

            Imgdonar.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
        }

        private async void BtnSubmit_Clicked(object sender, EventArgs e)
        {
            try
            {
                var imageArray = Array.Empty<byte>(); ;
                if (file == null || PickerCountry.SelectedIndex < 0 || PickerBloodGroup.SelectedIndex < 0 || EntName.Text == null || EntEmail.Text == null || EntPhone.Text == null)
                {
                    await DisplayAlert("Alert", "Please fill all fileds", "Cancel");
                    return;
                }

                try
                {
                    imageArray = FilesHelper.ReadFully(file.GetStream());
                    file.Dispose();
                }
                catch (System.ObjectDisposedException ex)
                {

                }

                var country = PickerCountry.Items[PickerCountry.SelectedIndex];
                var bloodGroup = PickerBloodGroup.Items[PickerBloodGroup.SelectedIndex];
                var dateString = DateTime.Now.ToString("yyyyMMdd");
                var bloodUser = new BloodUser()
                {
                    UserName = EntName.Text,
                    Email = EntEmail.Text,
                    Phone = EntPhone.Text,
                    BloodGroup = bloodGroup,
                    Country = country,
                    ImageArray = imageArray,
                    Date = int.Parse(dateString)
                };
                var apiServices = new ApiServices();
                var lstUsers = await apiServices.LatestBloodUser();
                foreach (BloodUser bloodUser1 in lstUsers)
                {
                    if (bloodUser1.Email == bloodUser.Email)
                    {
                        await DisplayAlert("Alert", "Email already exist", "Cancel");
                        return;
                    }
                    if (bloodUser1.UserName == bloodUser.UserName)
                    {
                        await DisplayAlert("Alert", "User name already exists", "Cancel");
                        return;
                    }
                    if (bloodUser1.Phone == bloodUser.Phone)
                    {
                        await DisplayAlert("Alert", "Phone already exists", "Cancel");
                        return;
                    }
                }
                var response = await apiServices.RegisterDonar(bloodUser);
                if (!response)
                {
                    await DisplayAlert("Alert", "Something wrong", "Cancel");
                }
                else
                {
                    await DisplayAlert("Hello", "Record has been added successfully", "Ok");
                    await Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.Message + " Please turn on your network", "Cancel");
            }
        }
    }
}