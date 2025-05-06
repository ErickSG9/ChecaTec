using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data;
using Xamarin.Forms;

namespace Test
{
    public partial class HistorialPage : ContentPage
    {
        public HistorialPage()
        {
            InitializeComponent();
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

            var operaciones = Database.ObtenerOperacionesPorPaciente(App.UsuarioActual.IdUsuario);
            ListaOperaciones.ItemsSource = operaciones;
        }
        private async void OnNuevoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AgregarOperacionPage());
        }
    }
}