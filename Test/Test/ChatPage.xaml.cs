using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Test
{
        public partial class ChatPage : ContentPage
        {
            public ChatPage(string nombre)
            {
                InitializeComponent();
                NombreDoctor.Text = nombre;
                CargarMensajesPrueba();
            }

            void CargarMensajesPrueba()
            {
                AgregarMensaje("Hola, ¿cómo te sientes hoy?", false);
                AgregarMensaje("Mejor, gracias doctor.", true);
            }

            void EnviarMensaje_Clicked(object sender, EventArgs e)
            {
                var texto = MensajeEntry.Text;
                if (!string.IsNullOrWhiteSpace(texto))
                {
                    AgregarMensaje(texto, true);
                    MensajeEntry.Text = "";
                }
            }

            void AgregarMensaje(string texto, bool esUsuario)
            {
                var mensajeLabel = new Label
                {
                    Text = texto,
                    BackgroundColor = esUsuario ? Color.LightGreen : Color.LightGray,
                    Padding = 10,
                    Margin = new Thickness(5),
                    HorizontalOptions = esUsuario ? LayoutOptions.End : LayoutOptions.Start,
                    TextColor = Color.Black
                };

                MensajesStack.Children.Add(mensajeLabel);
            }
        }
}
