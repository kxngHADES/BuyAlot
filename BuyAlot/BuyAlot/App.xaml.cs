using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BuyAlot.Pages;
using BuyAlot.Database;
using SQLite;
using System.IO;

namespace BuyAlot
{
    public partial class App : Application
    {
        public static DatabaseServices Database;
        public static int CurrentUserId {get; set;}
        public App()
        {
            InitializeComponent();
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BuyAlot.db3");
            Database = new DatabaseServices(databasePath);
            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static void NavigateToMainPage()
        {
            Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}
