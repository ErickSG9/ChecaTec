using System;
using System.Linq;
using Xamarin.Forms;
using Test.Data;

namespace Test
{
    public partial class PersonalPage : ContentPage
    {
        public PersonalPage()
        {
            InitializeComponent();
            CargarMedicos();
        }

        private void CargarMedicos()
        {
            var usuarios = Database.GetUsuariosPorRol("Médico");

            var medicos = usuarios.Select(u =>
            {
                var medico = Database.GetMedicoPorUsuarioId(u.IdUsuario);
                var genero = medico?.Genero?.ToLower() ?? "";

                string prefijo = genero == "femenino" ? "Dra." : "Dr.";
                string nombreCompleto = $"{prefijo} {u.Nombre} {u.Apellidos}";

                return new
                {
                    u.IdUsuario,
                    u.Nombre,
                    u.Apellidos,
                    NombreCompleto = nombreCompleto,
                    u.Email,
                    u.Telefono,
                    Especialidad = medico?.Especialidad ?? "Sin asignar",
                    HorarioAtencion = medico?.HorarioAtencion ?? "N/D",
                    Clinica = medico?.Clinica ?? "N/D",
                    Genero = medico?.Genero ?? "N/D"
                };
            }).ToList();


            ListaMedicos.ItemsSource = medicos;
        }

        private async void OnVerDetallesClicked(object sender, EventArgs e)
        {
            var boton = sender as Button;
            var medico = boton?.BindingContext;

            if (medico != null)
            {
                var tipo = medico.GetType();
                string nombre = tipo.GetProperty("NombreCompleto")?.GetValue(medico)?.ToString();
                string especialidad = tipo.GetProperty("Especialidad")?.GetValue(medico)?.ToString();
                string horario = tipo.GetProperty("HorarioAtencion")?.GetValue(medico)?.ToString();
                string clinica = tipo.GetProperty("Clinica")?.GetValue(medico)?.ToString();
                string genero = tipo.GetProperty("Genero")?.GetValue(medico)?.ToString();
                string email = tipo.GetProperty("Email")?.GetValue(medico)?.ToString();
                string telefono = tipo.GetProperty("Telefono")?.GetValue(medico)?.ToString();

                string mensaje = $"Especialidad: {especialidad}\n" +
                                 $"Horario: {horario}\n" +
                                 $"Clínica: {clinica}\n" +
                                 $"Género: {genero}\n" +
                                 $"Email: {email}\n" +
                                 $"Teléfono: {telefono}";

                await DisplayAlert(nombre, mensaje, "Cerrar");
            }
        }
    }
}
