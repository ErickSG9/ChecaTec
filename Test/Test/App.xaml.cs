using System;
using Test.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Test.Data;

namespace Test
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Database.InitializeDatabase();
            MainPage = new NavigationPage(new LoginPage());
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
    }
}
