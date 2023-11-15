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
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void DisplayUserDetails()
        {
            var currentUser = await App.Database.GetUserById(App.CurrentUserId);
            if (currentUser != null)
            {
                lblWelcome.Text = $"Welcome, {currentUser.FirstName} {currentUser.LastName}!";
            }
        }

        private void OnAddProductClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddProductPage());
        }

        private void OnViewProductsClicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new ViewProductsPage());
        }

        private void OnViewCartClicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new Cart());
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            // Clear the current user ID
            App.CurrentUserId = 0;

            // Navigate to the login page and remove previous pages from the navigation stack
            await Navigation.PushModalAsync(new LoginPage());
            Navigation.RemovePage(this);
        }
    }
}