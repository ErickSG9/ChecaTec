using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Test.Views;

namespace Test
{
    public partial class RegistroPage : ContentPage
    {
        public RegistroPage()
        {
            InitializeComponent();
        }

        private async void RegistrarUsuario_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Obtener los valores ingresados
                string nombre = entryNombre.Text;
                string email = entryEmail.Text;
                string contrasena = entryContrasena.Text;
                string rol = pickerRol.SelectedItem?.ToString();

                // Validación básica
                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) ||
                    string.IsNullOrEmpty(contrasena) || string.IsNullOrEmpty(rol))
                {
                    await DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
                    return;
                }

                // Registrar en SQLite
                //Database.RegistrarUsuario(nombre, email, contrasena, rol);

                await DisplayAlert("Éxito", "Usuario registrado correctamente", "OK");

                // Redirigir a la pantalla de inicio de sesión
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
