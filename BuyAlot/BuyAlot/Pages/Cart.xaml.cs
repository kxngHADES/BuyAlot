using BuyAlot.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuyAlot.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cart : ContentPage
    {
        private readonly DatabaseServices _database;
        private ObservableCollection<Product> _cartItems;

        public Cart(DatabaseServices database)
        {
            InitializeComponent();
            _database = database;
            _cartItems = new ObservableCollection<Product>();
            cartListView.ItemsSource = _cartItems;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadCartItems();
        }

        private async void LoadCartItems()
        {
            // Fetch all products in the cart for the current user (Assuming you have a user ID)
            int userId = App.CurrentUserId;
            var cartProducts = await _database.GetCartProducts(userId);

            _cartItems.Clear();
            foreach (var product in cartProducts)
            {
                _cartItems.Add(product);
            }
        }

        private async void OnRemoveClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is int productId)
            {
                // Implement logic to remove the product from the cart
                await _database.RemoveProductFromCart(productId);

                // Reload the cart items after removal
                LoadCartItems();
            }
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Product selectedProduct)
            {
                await Navigation.PushAsync(new ItemDetails(_database, selectedProduct.ProductId));
            }

            // Deselect the item to avoid highlighting
            ((ListView)sender).SelectedItem = null;
        }
    }
}