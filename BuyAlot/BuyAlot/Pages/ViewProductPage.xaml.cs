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
    public partial class ViewProductPage : ContentPage
    {
        public ViewProductPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Fetch all products from the database
            var allProducts = App.Database.GetAllProducts();

            // Set the list of products as the ItemsSource for the ListView
            productListView.ItemsSource = (System.Collections.IEnumerable)allProducts;
        }

        private void OnViewDetailsClicked(object sender, EventArgs e)
        {
            // Get the product ID from the tapped button's CommandParameter
            if (sender is Button btn && btn.CommandParameter is int productId)
            {
                // Navigate to the ViewProductPage with the selected product ID
                Navigation.PushAsync(new ItemDetails(App.Database, productId));
            }
        }
    }
}