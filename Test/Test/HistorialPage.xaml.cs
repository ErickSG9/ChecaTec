using System;
using System.Collections.Generic;
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
                .OrderByDescending(c => c.Fecha).ToList();

            foreach (var consulta in consultas)
            {
                var doctor = Database.GetUsuarioPorId(consulta.IdDoctor);
                consulta.NombreDoctor = doctor?.Nombre ?? "Desconocido";
            }

            var operaciones = Database.ObtenerOperacionesPorPaciente(idUsuario)
                .OrderByDescending(c => c.Fecha).ToList();
            var paciente = Database.GetPacientePorUsuario(idUsuario);
            var recetas = paciente != null
                ? Database.ObtenerRecetasPorPaciente(paciente.IdPaciente)
                : new List<Receta>();
            if (App.UsuarioActual.Rol == "Paciente")
            {
                recetas = recetas
                    .Where(r => r.Activa == false)
                    .OrderByDescending(r => r.FechaEmision)
                    .ToList();
            }
            else
            {
                recetas = recetas
                    .OrderByDescending(r => r.FechaEmision)
                    .ToList();
            }

            foreach (var receta in recetas)
            {
                var doctor = Database.GetUsuarioPorId(receta.IdProfesional);
                receta.Profesional = doctor?.Nombre ?? "Desconocido";
            }
            ListaRecetas.ItemsSource = recetas;
            ListaConsultas.ItemsSource = consultas;
            ListaOperaciones.ItemsSource = operaciones;

            MostrarConsultas(false);
            MostrarOperaciones(false); 
            MostrarRecetas(true);
            FiltroHistorial.SelectedItem = 2; 
            BotonAgregar.IsVisible = App.UsuarioActual.Rol == "Médico";
        }

        private void FiltroHistorial_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccion = FiltroHistorial.SelectedItem as string;

            switch (seleccion)
            {
                case "Consultas":
                    MostrarConsultas(true);
                    MostrarOperaciones(false);
                    MostrarRecetas(false);
                    break;
                case "Operaciones":
                    MostrarConsultas(false);
                    MostrarOperaciones(true);
                    MostrarRecetas(false);
                    break;
                case "Recetas":
                    MostrarConsultas(false);
                    MostrarOperaciones(false);
                    MostrarRecetas(true);
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
        private void MostrarRecetas(bool visible)
        {
            RecetasLayout.IsVisible = visible;
        }

        private async void OnPerfilClicked(object sender, EventArgs e)
        {
            var receptor = Database.GetUsuarioPorId(receptorId);
            var paciente = Database.GetPacientePorUsuario(receptorId); 

            await Navigation.PushAsync(new PacientePage(receptor, paciente));
        }

        private async void OnNuevoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Agregar(receptorId));
        }
    }
}
