using System;
using Test.Data;
using Test.Models;
using Xamarin.Forms;

namespace Test
{
    public partial class AgregarOperacionPage : ContentPage
    {
        public AgregarOperacionPage()
        {
            InitializeComponent();
        }

        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tipoEntry.Text) ||
                string.IsNullOrWhiteSpace(cirujanoEntry.Text) ||
                string.IsNullOrWhiteSpace(quirofanoEntry.Text) ||
                estadoPicker.SelectedIndex == -1)
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
                Estado = estadoPicker.SelectedItem.ToString(),
                Observaciones = observacionesEditor.Text,
                IdUsuario = App.UsuarioActual.IdUsuario
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
