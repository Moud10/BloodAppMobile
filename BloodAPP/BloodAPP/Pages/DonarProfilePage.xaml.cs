using BloodAPP.Models;
using Plugin.Messaging;
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
    public partial class DonarProfilePage : ContentPage
    {
        private string _email;
        private string _phoneNumber;
        public DonarProfilePage(BloodUser bloodUser)
        {
            InitializeComponent();
            LblDonarName.Text = bloodUser.UserName;
            ImgDonar.Source = bloodUser.imgTemp;
            LblBloodGroup.Text = bloodUser.BloodGroup;
            LblCountry.Text = bloodUser.Country;
            _email = bloodUser.Email;
            _phoneNumber = bloodUser.Phone;
        }

        private void TapPhone_Tapped(object sender, EventArgs e)
        {
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(_phoneNumber);
        }

        private void TapEmail_Tapped(object sender, EventArgs e)
        {
            var emailMessenger = CrossMessaging.Current.EmailMessenger;
            if (emailMessenger.CanSendEmail)
            {
                // Send simple e-mail to single receiver without attachments, bcc, cc etc.
                emailMessenger.SendEmail("to.plugins@xamarin.com", "Xamarin Messaging Plugin", "Well hello there from Xam.Messaging.Plugin");
            }
        }
    }
}