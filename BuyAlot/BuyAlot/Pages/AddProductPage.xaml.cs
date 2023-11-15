using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuyAlot.Database;
using Plugin.Media;
using Plugin.Media.Abstractions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuyAlot.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProductPage : ContentPage
    {
        private string imagePath;
        public AddProductPage()
        {
            InitializeComponent();
        }
        private async void OnUploadImageClicked(object sender, EventArgs e)
        {
            // Use the Media Plugin to pick an image from the device
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No Upload", "Picking a photo is not supported.", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Medium
            });

            if (file == null)
                return;

            // Set the image preview
            imgPreview.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                imagePath = file.Path; // Save the image path for later use
                return stream;
            });
        }

        private void OnAddProductClicked(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(txtProductName.Text) ||
                string.IsNullOrWhiteSpace(txtProductType.Text) ||
                string.IsNullOrWhiteSpace(txtProductPrice.Text) ||
                string.IsNullOrWhiteSpace(txtDescription.Text) ||
                string.IsNullOrWhiteSpace(imagePath))
            {
                DisplayAlert("Error", "Please fill in all fields and upload an image.", "OK");
                return;
            }

            // Parse product price
            if (!decimal.TryParse(txtProductPrice.Text, out decimal productPrice))
            {
                DisplayAlert("Error", "Invalid product price.", "OK");
                return;
            }

            // Create a new Product object
            var newProduct = new Product
            {
                ProductName = txtProductName.Text,
                ProductType = txtProductType.Text,
                ProductPrice = productPrice,
                UploadedByUserId = App.CurrentUserId,// Set the user ID here,
                ImagePath = imagePath,
                Description = txtDescription.Text
            };

            // Add the product to the database
            _ = App.Database.InsertProduct(newProduct);

            DisplayAlert("Success", "Product added successfully.", "OK");
            Navigation.PopAsync(); // Navigate back after adding the product
        }

        private void OnBackClicked(object sender, EventArgs e)
        {
            // Handle back navigation
            Navigation.PopAsync();
        }
    }
}