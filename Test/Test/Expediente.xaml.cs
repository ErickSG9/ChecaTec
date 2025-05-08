using System;
using Xamarin.Forms;

namespace Test
{
    public partial class ExpedientePage : ContentPage
    {
        public ExpedientePage()
        {
            InitializeComponent();
        }
        private async void OnEstadisticasClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EstadisticasPage());
        }
        private async void OnHistorialClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HistorialPage(App.UsuarioActual.IdUsuario));
        }
    }
}