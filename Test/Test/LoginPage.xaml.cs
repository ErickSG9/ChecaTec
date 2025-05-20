using System;
using Test.Data;
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
            string email = entryUsuario.Text;
            string clave = entryContrasena.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(clave))
            {
                await DisplayAlert("Error", "Ingrese correo y contraseña", "OK");
                return;
            }

            var usuario = Database.ValidarUsuario(email, clave);

            if (usuario != null)
            {
                App.UsuarioActual = usuario;
                await DisplayAlert("Bienvenido", $"Acceso correcto, {usuario.Nombre}", "Continuar");
                if (usuario.Rol == "Médico")
                {
                    Application.Current.MainPage = new NavigationPage(new MensajesPage());
                }
                else if (usuario.Rol == "Paciente")
                {
                    Application.Current.MainPage = new NavigationPage(new MainPageP());
                }
            }
            else
            {
                await DisplayAlert("Error", "Correo o contraseña incorrectos", "Intentar de nuevo");
            }
        }

        private async void IrARegistro_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistroPage());
        }
        private void MostrarOcultarContrasena_Clicked(object sender, EventArgs e)
        {
            entryContrasena.IsPassword = !entryContrasena.IsPassword;
        }
    }
}
