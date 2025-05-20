using Test.Views;
using Xamarin.Forms;
using Test.Data;
using Test.Models;

namespace Test
{
    public partial class App : Application
    {
        public static Usuario UsuarioActual { get; set; }
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
