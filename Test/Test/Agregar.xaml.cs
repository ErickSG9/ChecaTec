using System;
using Test.Data;
using Test.Models;
using Xamarin.Forms;

namespace Test
{
    public partial class Agregar : ContentPage
    {
        private int receptorId;

        public Agregar(int idReceptor)
        {
            InitializeComponent();
            receptorId = idReceptor;
        }
        private void Generar_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccion = Generar.SelectedItem as string;

            // Ocultar todo primero
            ConsultaLayout.IsVisible = false; 
            OperacionLayout.IsVisible = false;

            if (seleccion == "Consulta")
            {
                ConsultaLayout.IsVisible = true;
            }
            if (seleccion == "Operacion")
            {
                OperacionLayout.IsVisible = true;
            }

            // Aquí podrías agregar más casos para Receta y Operación si lo deseas
        }
        private async void OnGuardarCClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(motivoEntry.Text) || estadoCPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Completa todos los campos obligatorios.", "OK");
                return;
            }

            var nuevaConsulta = new Consulta
            {
                Fecha = DateTime.Today,
                Hora = DateTime.Now.ToString("hh:mm tt"), 
                Motivo = motivoEntry.Text,
                Estado = estadoCPicker.SelectedItem.ToString(),
                PresionArterial = presionEntry.Text,
                FrecuenciaCardiaca = int.TryParse(frecuenciaEntry.Text, out int fc) ? fc : 0,
                Temperatura = double.TryParse(temperaturaEntry.Text, out double temp) ? temp : 0.0,
                Observaciones = observacionesCEditor.Text,
                IdUsuario = receptorId,
                IdDoctor = App.UsuarioActual.IdUsuario 
            };

            try
            {
                Database.InsertarConsulta(nuevaConsulta);
                await DisplayAlert("Éxito", "Consulta registrada correctamente.", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo guardar la consulta.\n{ex.Message}", "OK");
            }
        }
        private async void OnGuardarOClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tipoEntry.Text) ||
                string.IsNullOrWhiteSpace(cirujanoEntry.Text) ||
                string.IsNullOrWhiteSpace(quirofanoEntry.Text) ||
                estadoOPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Completa todos los campos obligatorios.", "OK");
                return;
            }

            var nuevaOperacion = new Operacion
            {
                Fecha = DateTime.Today,
                Tipo = tipoEntry.Text,
                Cirujano = cirujanoEntry.Text,
                Hora = horaPicker.Time.ToString(@"hh\:mm") + (horaPicker.Time.Hours < 12 ? " AM" : " PM"),
                Quirofano = quirofanoEntry.Text,
                Estado = estadoOPicker.SelectedItem.ToString(),
                Observaciones = observacionesOEditor.Text,
                IdUsuario = receptorId
            };

            try
            {
                Database.InsertarOperacion(nuevaOperacion);
                await DisplayAlert("Éxito", "Operación registrada correctamente.", "OK");
                await Navigation.PopAsync(); // Regresar
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo guardar la operación.\n{ex.Message}", "OK");
            }
        }
    }
}
