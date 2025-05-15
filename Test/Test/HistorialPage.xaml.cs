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
            receptorId = idUsuario;
            FiltroHistorial.SelectedIndexChanged += FiltroHistorial_SelectedIndexChanged;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            int idUsuario = receptorId != 0 ? receptorId : App.UsuarioActual.IdUsuario;

            var consultas = Database.ObtenerConsultasPorPaciente(idUsuario)
                .OrderByDescending(c => c.Fecha)
                .ToList();

            foreach (var consulta in consultas)
            {
                var doctor = Database.GetUsuarioPorId(consulta.IdDoctor);
                consulta.NombreDoctor = doctor?.Nombre ?? "Desconocido";
            }

            var operaciones = Database.ObtenerOperacionesPorPaciente(idUsuario);

            ListaConsultas.ItemsSource = consultas;
            ListaOperaciones.ItemsSource = operaciones;

            // Mostrar ambos por defecto
            MostrarConsultas(true);
            MostrarOperaciones(true);
            FiltroHistorial.SelectedIndex = 2; // Ambos
        }

        private void FiltroHistorial_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccion = FiltroHistorial.SelectedItem as string;

            switch (seleccion)
            {
                case "Consultas":
                    MostrarConsultas(true);
                    MostrarOperaciones(false);
                    break;
                case "Operaciones":
                    MostrarConsultas(false);
                    MostrarOperaciones(true);
                    break;
            }
        }

        private void MostrarConsultas(bool visible)
        {
            ConsultasLayout.IsVisible = visible;
        }

        private void MostrarOperaciones(bool visible)
        {
            OperacionesLayout.IsVisible = visible;
        }

        private async void OnEstadisticasClicked(object sender, EventArgs e)
        {
            var receptor = Database.GetUsuarioPorId(receptorId);
            var paciente = Database.GetPacientePorId(receptor.IdUsuario);
            await Navigation.PushAsync(new PacientePage(receptor, paciente));
        }

        private async void OnNuevoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Agregar(receptorId));
        }
    }
}
