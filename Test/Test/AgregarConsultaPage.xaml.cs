using System;
using Test.Data;
using Test.Models;
using Xamarin.Forms;

namespace Test
{
    public partial class AgregarConsultaPage : ContentPage
    {
        private int receptorId;

        public AgregarConsultaPage(int idReceptor)
        {
            InitializeComponent();
            receptorId = idReceptor;
        }

        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(motivoEntry.Text) || estadoPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Completa todos los campos obligatorios.", "OK");
                return;
            }

            var nuevaConsulta = new Consulta
            {
                Fecha = DateTime.Today,
                Hora = DateTime.Now.ToString("hh:mm tt"), 
                Motivo = motivoEntry.Text,
                Estado = estadoPicker.SelectedItem.ToString(),
                PresionArterial = presionEntry.Text,
                FrecuenciaCardiaca = int.TryParse(frecuenciaEntry.Text, out int fc) ? fc : 0,
                Temperatura = double.TryParse(temperaturaEntry.Text, out double temp) ? temp : 0.0,
                Observaciones = observacionesEditor.Text,
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
    }
}
