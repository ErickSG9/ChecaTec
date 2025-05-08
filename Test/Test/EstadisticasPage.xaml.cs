using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Test
{
    public partial class EstadisticasPage : ContentPage
    {
        public EstadisticasPage()
        {
            InitializeComponent();
        }
        private async void OnHistorialClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HistorialPage(App.UsuarioActual.IdUsuario));
        }
    }
}