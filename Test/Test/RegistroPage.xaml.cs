using System;
using Xamarin.Forms;
using Test.Views;
using Test.Data;
using Test.Models;

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
                // Datos comunes
                string nombre = entryNombre.Text;
                string apellidos = entryApellidos.Text;
                string email = entryEmail.Text;
                string contrasena = entryContrasena.Text;
                string rol = pickerRol.SelectedItem?.ToString();
                string especialidad = "";
                string genero = null;
                double peso = 0, altura = 0;
                string alergias = "", antecedentes = "", medicamentos = "", vacunas = "", discapacidad = "";
                DateTime fechaNacimiento = fechaNacimientoPicker.Date;
                string numeroSeguro = entryNumeroSeguro.Text;
                string clinica = "", horario = "", generoM = "";

                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(email) ||
                    string.IsNullOrWhiteSpace(contrasena) || string.IsNullOrWhiteSpace(rol) ||
                    string.IsNullOrWhiteSpace(apellidos))
                {
                    await DisplayAlert("Error", "Todos los campos obligatorios deben estar llenos.", "OK");
                    return;
                }

                if (!int.TryParse(entryEdad.Text, out int edad))
                {
                    await DisplayAlert("Error", "Edad inválida", "OK");
                    return;
                }

                if (!double.TryParse(entryTelefono.Text, out double telefono))
                {
                    await DisplayAlert("Error", "Teléfono inválido", "OK");
                    return;
                }

                // Datos de emergencia
                string nomE = entryNomE.Text;
                string parE = entryParE.Text;
                string telEmergencia = entryTelE.Text;

                if (!string.IsNullOrWhiteSpace(telEmergencia) && !long.TryParse(telEmergencia, out _))
                {
                    await DisplayAlert("Error", "Teléfono de emergencia inválido", "OK");
                    return;
                }

                // Datos específicos por rol
                if (rol == "Paciente")
                {
                    genero = pickerGenero.SelectedItem?.ToString();
                    double.TryParse(entryPeso.Text, out peso);
                    double.TryParse(entryAltura.Text, out altura);
                    alergias = editorAlergias.Text;
                    antecedentes = editorAntecedentes.Text;
                    medicamentos = editorMedicamentos.Text;
                    vacunas = editorVacunas.Text;
                    discapacidad = editorDiscapacidad.Text;
                }
                else if (rol == "Médico")
                {
                    especialidad = entryEspecialidad.Text?.Trim();
                    horario = entryHorario.Text?.Trim();
                    clinica = entryClinica.Text?.Trim();
                    generoM = pickerGeneroMedico.SelectedItem?.ToString();

                    if (string.IsNullOrWhiteSpace(especialidad))
                    {
                        await DisplayAlert("Error", "Ingrese una especialidad para el médico.", "OK");
                        return;
                    }
                }

                // Registrar usuario
                Database.RegistrarUsuario(nombre, apellidos, edad, telefono.ToString(), genero, email, contrasena, rol);

                // Obtener ID del nuevo usuario
                var usuario = Database.GetUsuarioPorId(
                    Database.GetConnection().Table<Usuario>()
                    .Where(u => u.Email == email)
                    .OrderByDescending(u => u.IdUsuario)
                    .First().IdUsuario
                );

                // Registrar paciente
                if (rol == "Paciente")
                {
                    Database.RegistrarPaciente(
                        usuario.IdUsuario,
                        fechaNacimiento,
                        numeroSeguro,
                        peso,
                        altura,
                        alergias,
                        antecedentes,
                        medicamentos,
                        vacunas,
                        discapacidad, nomE, parE, telEmergencia
                    );
                }

                // Registrar médico
                if (rol == "Médico")
                {
                    Database.RegistrarMedico(
                        usuario.IdUsuario,
                        especialidad,
                        horario,
                        clinica
                    );
                }

                await DisplayAlert("Éxito", "Usuario registrado correctamente", "OK");
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
            pacienteLayout.IsVisible = rolSeleccionado == "Paciente";
            emergenciaLayout.IsVisible = rolSeleccionado == "Paciente";
            medicoLayout.IsVisible = rolSeleccionado == "Médico";
        }
    }
}
