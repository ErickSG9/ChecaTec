using System;
using Test.Data;
using Xamarin.Forms;

namespace Test
{
    public partial class HistorialPage : ContentPage
    {
        private int receptorId;
        public HistorialPage(int idUsuario)
        {
            InitializeComponent();
            CargarOperaciones(idUsuario); 
            receptorId = idUsuario;
        }
        private void CargarOperaciones(int idUsuario)
        {
            var operaciones = Database.ObtenerOperacionesPorPaciente(idUsuario);
            ListaOperaciones.ItemsSource = operaciones; // Asegúrate de tener este CollectionView en el XAML
        }
        private async void OnEstadisticasClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EstadisticasPage());
        }
        private async void OnMainClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPageP());
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            int idPaciente;

            if (App.UsuarioActual.Rol == "Médico" && receptorId != 0)
            {
                idPaciente = receptorId;
            }
            else
            {
                idPaciente = App.UsuarioActual.IdUsuario;
            }

            var operaciones = Database.ObtenerOperacionesPorPaciente(idPaciente);
            ListaOperaciones.ItemsSource = operaciones;
        }
        private async void OnNuevoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AgregarOperacionPage(receptorId));
        }
    }
}