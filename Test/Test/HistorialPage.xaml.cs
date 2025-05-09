using System;
using System.Linq;
using Test.Data;
using Test.Models;
using Xamarin.Forms;

namespace Test
{
    public partial class HistorialPage : ContentPage
    {
        private int receptorId;
        public HistorialPage(int idUsuario)
        {
            InitializeComponent();
            CargarConsultas(idUsuario); 
            receptorId = idUsuario;
        }
        private void CargarConsultas(int idUsuario)
        {
            var consultas = Database.ObtenerConsultasPorPaciente(idUsuario);
            ListaConsultas.ItemsSource = consultas; 
        }
        private async void OnEstadisticasClicked(object sender, EventArgs e)
        {
            var receptor = Database.GetUsuarioPorId(receptorId);
            await Navigation.PushAsync(new PacientePage(receptor));
        }
        private async void OnPerfilClicked(object sender, EventArgs e)
        {
            var receptor = Database.GetUsuarioPorId(receptorId);
            await Navigation.PushAsync(new PacientePage(receptor));
        }
        private async void OnMainClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPageP());
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var usuarioActual = App.UsuarioActual; 
            int idUsuario = usuarioActual.IdUsuario;

            if (App.UsuarioActual.Rol == "Médico" && receptorId != 0)
            {
                idUsuario = receptorId;
            }
            else
            {
                idUsuario = App.UsuarioActual.IdUsuario;
            }

            var consultas = Database.ObtenerConsultasPorPaciente(idUsuario)
                        .OrderByDescending(c => c.Fecha)
                        .ToList();
            foreach (var consulta in consultas)
            {
                var doctor = Database.GetUsuarioPorId(consulta.IdDoctor);
                consulta.NombreDoctor = doctor?.Nombre ?? "Desconocido";
            }
            ListaConsultas.ItemsSource = null;
            ListaConsultas.ItemsSource = consultas;
        }
        private async void OnNuevoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AgregarConsultaPage(receptorId));
        }
    }
}