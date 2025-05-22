using System;
using System.Linq;
using Test.Data;
using Test.Models;
using Xamarin.Forms;

namespace Test
{
    public partial class RecetaPage : ContentPage
    {
        public RecetaPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var usuario = App.UsuarioActual;
            if (usuario == null)
            {
                DisplayAlert("Error", "Usuario no autenticado.", "OK");
                return;
            }

            var paciente = Database.GetPacientePorUsuario(usuario.IdUsuario);
            if (paciente == null)
            {
                DisplayAlert("Error", "No se encontró el paciente relacionado.", "OK");
                return;
            }

            var recetas = Database.ObtenerRecetasPorPaciente(paciente.IdPaciente)
                                  .Where(r => r.Activa == true)
                                  .ToList();

            foreach (var receta in recetas)
            {
                var doctor = Database.GetUsuarioPorId(receta.IdProfesional);
                receta.Profesional = doctor?.Nombre ?? "Desconocido";
            }

            

            ListaRecetas.ItemsSource = recetas;
        }
        private async void OnDesactivarRecetaClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var receta = button?.CommandParameter as Receta;

            if (receta == null)
                return;

            bool confirmacion = await DisplayAlert("Confirmar", "¿Deseas dar por completado su tratamiento?", "Sí", "No");

            if (confirmacion)
            {
                receta.Activa = false;

                var db = Data.Database.GetConnection();
                db.Update(receta);

                await DisplayAlert("Éxito", "La receta completada.", "OK");

                // Recargar la lista
                OnAppearing();
            }
        }

    }
}
