using BuyAlot.Database;
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
    public partial class ItemDetails : ContentPage
    {
        private readonly DatabaseServices _database;
        private readonly int _productId;

        public ItemDetails(DatabaseServices database, int productId)
        {
            InitializeComponent();
            _database = database;
            _productId = productId;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Fetch the details of the selected product
            var product = await _database.GetProductById(_productId);

            // Set the product as the BindingContext for data binding
            BindingContext = product;
        }

        private async void OnAddToCartClicked(object sender, EventArgs e)
        {
            int Quantity = Int32.Parse(txtQuantity.Text);
            // Fetch the details of the selected product
            var product = await _database.GetProductById(_productId);

            // Create an OrderItem to add to the cart
            var orderItem = new OrderItem
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                Quantity = Quantity,
                UserId = App.CurrentUserId
            };

            // Add the product to the cart
            var result = await _database.AddToCart(orderItem);

            if (result > 0)
            {
                // Display a confirmation message
                await DisplayAlert("Added to Cart", "Product added to cart successfully.", "OK");
            }
            else
            {
                // Display an error message if adding to cart fails
                await DisplayAlert("Error", "Failed to add the product to the cart.", "OK");
            }
        }
    }
}