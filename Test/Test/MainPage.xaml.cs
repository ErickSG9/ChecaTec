using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Views;
using Xamarin.Forms;

namespace Test
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void OnMensajesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MensajesPage());
        }
        private async void OnDBClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UsuariosPage());
        }

        private async void OnEstadisticasClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EstadisticasPage());
        }
        private async void OnHistorialClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HistorialPage());
        }
        private async void OnRecetaClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecetaPage());
        }

        private async void OnPersonalClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PersonalPage());
        }
        private async void OnUsersClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UsuariosPage());
        }
        private async void CerrarSesion_Clicked(object sender, EventArgs e)
        {
            bool respuesta = await DisplayAlert("Cerrar sesión", "¿Está seguro de que desea cerrar sesión?", "Sí", "Cancelar");

            if (respuesta) // Si elige "Sí"
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage()); // Redirigir al login
            }
        }
    }
}
