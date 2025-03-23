using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Test
{
    public partial class MensajesPage : ContentPage
    {
        public MensajesPage()
        {
            InitializeComponent();
            CargarMensajes();
        }

        // Modelo de mensaje
        public class Mensaje
        {
            public string Foto { get; set; }
            public string Nombre { get; set; }
            public string UltimoMensaje { get; set; }
            public string Hora { get; set; }
        }

        // Lista de ejemplo
        private void CargarMensajes()
        {
            var lista = new List<Mensaje>
            {
                new Mensaje { Foto = "doctor1.png", Nombre = "Dr. Ramírez", UltimoMensaje = "Nos vemos a las 10", Hora = "9:45" },
                new Mensaje { Foto = "doctor2.png", Nombre = "Dra. López", UltimoMensaje = "Receta enviada", Hora = "Ayer" },
                new Mensaje { Foto = "doctor3.png", Nombre = "Dr. Pérez", UltimoMensaje = "Gracias por la info", Hora = "Lun" }
            };

            MensajesList.ItemsSource = lista;
        }

        // Navegación al chat
        private async void MensajesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Mensaje seleccionado)
            {
                await Navigation.PushAsync(new ChatPage(seleccionado.Nombre));
                MensajesList.SelectedItem = null; // Deselecciona después de abrir
            }
        }
    }
}
