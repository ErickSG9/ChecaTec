using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Test.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void IniciarSesion_Clicked(object sender, System.EventArgs e)
        {
            string usuario = entryUsuario.Text;
            string clave = entryContrasena.Text;

            if (usuario == "admin" && clave == "1234")
            {
                await DisplayAlert("Bienvenido", "Acceso correcto", "Continuar");

                // Navegar a MainPage    (Checar si esto si se queda)
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                await DisplayAlert("Error", "Credenciales incorrectas", "Intentar de nuevo");
            }
        }

        private async void Registrarse_Clicked(object sender, System.EventArgs e)
        {
            await DisplayAlert("Registro", "Redirigiendo al registro...", "OK");
        }
        private void MostrarOcultarContrasena_Clicked(object sender, EventArgs e)
        {
            entryContrasena.IsPassword = !entryContrasena.IsPassword;
        }
    }
}
