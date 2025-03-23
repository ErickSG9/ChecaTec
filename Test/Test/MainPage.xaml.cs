using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private async void OnEstadisticasClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EstadisticasPage());
        }

        private async void OnRecetaClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecetaPage());
        }

        private async void OnPersonalClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PersonalPage());
        }
    }
}
