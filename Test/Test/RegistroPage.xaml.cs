using System;
using Xamarin.Forms;
using Test.Views;
using Test.Data;

namespace Test
{
    public partial class RegistroPage : ContentPage
    {
        public RegistroPage()
        {
            InitializeComponent();
        }
        private void VerContrasena_Clicked(object sender, EventArgs e)
        {
            entryContrasena.IsPassword = !entryContrasena.IsPassword;
            btnVerContrasena.Text = entryContrasena.IsPassword ? "👁️" : "🙈";
        }
        private async void RegistrarUsuario_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Obtener los valores obligatorios
                string nombre = entryNombre.Text;
                string apellidos = entryApellidos.Text;
                string email = entryEmail.Text;
                string contrasena = entryContrasena.Text;
                string rol = pickerRol.SelectedItem?.ToString();

                // Validación básica
                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) ||
                    string.IsNullOrEmpty(contrasena) || string.IsNullOrEmpty(rol) ||
                    string.IsNullOrEmpty(apellidos))
                {
                    await DisplayAlert("Error", "Todos los campos obligatorios deben estar llenos.", "OK");
                    return;
                }

                if (!int.TryParse(entryEdad.Text, out int edad))
                {
                    await DisplayAlert("Error", "Edad inválida", "OK");
                    return;
                }

                if (!int.TryParse(entryTelefono.Text, out int telefono))
                {
                    await DisplayAlert("Error", "Teléfono inválido", "OK");
                    return;
                }

                // Datos de emergencia (opcionales)
                string nomE = string.Empty;
                string parE = string.Empty;
                int telE = 0;

                if (rol == "Paciente")
                {
                    nomE = entryNomE.Text;
                    parE = entryParE.Text;

                    // Si alguno de estos campos tiene algo, validamos
                    if (!string.IsNullOrWhiteSpace(entryTelE.Text))
                    {
                        if (!int.TryParse(entryTelE.Text, out telE))
                        {
                            await DisplayAlert("Error", "Teléfono de emergencia inválido", "OK");
                            return;
                        }
                    }
                }

                // Registrar en SQLite
                Database.RegistrarUsuario(nombre, apellidos, edad, telefono, email, contrasena, rol, nomE, parE, telE);

                await DisplayAlert("Éxito", "Usuario registrado correctamente", "OK");

                // Redirigir al Login
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void pickerRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            var rolSeleccionado = pickerRol.SelectedItem?.ToString();

            // Mostrar campos de emergencia solo si el usuario es Paciente
            if (rolSeleccionado == "Paciente")
                emergenciaLayout.IsVisible = true;
            else
                emergenciaLayout.IsVisible = false;
        }
    }
}
