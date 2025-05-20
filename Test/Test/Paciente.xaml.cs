using System;
using Test.Data;
using Test.Models;
using Xamarin.Forms;

namespace Test
{
    public partial class PacientePage : ContentPage
    {
        private readonly int _idUsuario; 
        private readonly int _idPaciente;
        public PacientePage(Usuario usuario, Pacientes paciente)
        {
            InitializeComponent();
            if (paciente == null )
            {
                DisplayAlert("Error", "Los datos del paciente están vacíos", "OK");
                return;
            }
            _idUsuario = usuario.IdUsuario;
            _idPaciente = paciente.IdPaciente;


        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Obtener datos de Usuario y Paciente
            var usuario = Database.GetUsuarioPorId(_idUsuario);
            var paciente = Database.GetPacientePorId(_idPaciente);
            await DisplayAlert("DEBUG", $"Usuario: {usuario?.IdUsuario} / Paciente: {paciente?.NumeroSeguro}", "OK");

            if (usuario != null && paciente != null)
            {
                var vm = new PerfilPacienteViewModel
                {
                    IdUsuario = usuario.IdUsuario,
                    Nombre = usuario.Nombre,
                    Apellidos = usuario.Apellidos,
                    Email = usuario.Email,
                    Contrasena = usuario.Contrasena,
                    Rol = usuario.Rol,
                    FechaRegistro = usuario.FechaRegistro,

                    Genero = usuario.Genero,
                    Edad = usuario.Edad,
                    Telefono = usuario.Telefono,
                    Peso = paciente.Peso,
                    Altura = paciente.Altura,
                    Alergias = paciente.Alergias,
                    AntecedentesClinicos = paciente.AntecedentesClinicos,
                    Medicamentos = paciente.Medicamentos,
                    Vacunas = paciente.Vacunas,
                    Discapacidad = paciente.Discapacidad,

                    NombreE = paciente.NombreE,
                    ParentescoE = paciente.ParentescoE,
                    TelefonoE = paciente.TelefonoE
                };

                BindingContext = vm;
            }
        }

        private async void OnHistorialClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}