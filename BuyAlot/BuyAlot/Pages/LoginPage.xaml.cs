using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuyAlot.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            btnRegister.Clicked += (s,e) => Navigation.PushModalAsync(new RegistrationPage());
            btnLogin.Clicked += OnLoginClick; ;
        }
        //Show password check box
        private void OnCheckChange(Object Sender, EventArgs e)
        {
            if (cbxPassword.IsChecked == true)
            {
                txtPassword.IsPassword = false;
            }
            else
            {
                txtPassword.IsPassword = true;
            }
        }

        private async void OnLoginClick(Object Sender, EventArgs E)
        {
            var user = await App.Database.GetUser(txtUsername.Text, txtPassword.Text);

            if (user != null)
            {
                App.CurrentUserId = user.UserId;
                App.NavigateToMainPage();
            }
            else
            {
                await DisplayAlert("Error", "Invalid username or Password", "OK");
            }
        }
    }
}