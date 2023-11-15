using BuyAlot.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuyAlot.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void OnRegisterClicked(object sender, EventArgs e)
        {
            //Check user inputs
            if (string.IsNullOrEmpty(txtFirstName.Text) ||
                string.IsNullOrEmpty(txtLastName.Text) ||
                string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(txtPassword.Text) ||
                string.IsNullOrEmpty(txtAddress.Text) ||
                string.IsNullOrEmpty(txtUsername.Text) ||
                string.IsNullOrEmpty(txtNumber.Text))
            {
                //Display alert message if there is an empty field
                DisplayAlert("Error", "Please Fill in all fields", "OK");
                return;
            }
            //Validate email format 
            if (!IsValidEmail(txtEmail.Text))
            {
                DisplayAlert("Error", "Please enter a valid email address", "OK");
                return;
            }

            //Validate Password Strength
            if (!IsValidPassword(txtPassword.Text))
            {
                DisplayAlert("Error", "Password must be at least 8 characters and include at least 1 number, 1 special character, and both upper and lower case letters", "OK");
                return;
            }
            if (!IsValidPhoneNumber(txtNumber.Text))
            {
                DisplayAlert("Error", "Please enter a valid phone number (at least 10 digits, numbers only)", "OK");
                return;
            }
            //Create user object with input data
            var newUser = new User
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Email = txtEmail.Text,
                Password = txtPassword.Text,
                Address = txtAddress.Text,
                Username = txtUsername.Text,
                PhoneNumber = txtNumber.Text
            };
            try
            {
                _ = App.Database.InsertUser(newUser);
                //Infor success
                DisplayAlert("Success", "Account Succefully Created", "Ok");
                //Go to login
                Navigation.PushModalAsync(new LoginPage());
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", "Error..." + ex.Message, "OK");
                return;
            }
        }

        private void OnLoginClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new LoginPage());
        }
        #region Password Validation
        private bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, @"^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,}$");
            //Ensure password must be atleast 8 characters ensure it has atleast 1 number, 1 special character, and both upper and lower case letters
        }
        #endregion
        #region Email Validation
        private bool IsValidEmail(string email)
        {
            try //Attempt to create an MailAddress object using the email
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email; //Compare the object to the email, if they match it will return that its true
            }
            catch
            {
                return false;//If email is not valid it will return false
            }
        }
        #endregion
        #region Phone Number Validation
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^\d{10,}$"); // [ ^ ] means start of string [ \d ] means digits in range of 0-9 [ {10,} ] means atleast 10 [ $ ] means end of string 
        }
        #endregion
    }
}