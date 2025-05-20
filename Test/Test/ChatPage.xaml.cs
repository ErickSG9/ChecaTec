using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Test.Models;
using Test.Data;

namespace Test
{
    public partial class ChatPage : ContentPage
    {
        private Usuario emisor;
        private Usuario receptor;
        private int IdEmisor;
        private int IdReceptor;

        public ObservableCollection<Chat> Mensajes { get; set; }

        public ChatPage(int idEmisor, int idReceptor)
        {
            InitializeComponent();
            IdEmisor = idEmisor;
            IdReceptor = idReceptor;

            emisor = Database.GetUsuarioPorId(IdEmisor);
            receptor = Database.GetUsuarioPorId(IdReceptor);

            if (receptor != null)
            {
                if (emisor?.Rol == "Paciente")
                {
                    var genero = receptor.Genero?.ToLower();
                    Title = genero == "femenino" ? $"Dra. {receptor.Nombre}" : $"Dr. {receptor.Nombre}";
                }
                else
                {
                    Title = receptor.Nombre;
                }
            }
            else
            {
                Title = "Chat";
            }

            Mensajes = new ObservableCollection<Chat>();
            MensajesListView.ItemsSource = Mensajes;

            CargarMensajes();

            // Mostrar botón solo si es médico
            if (App.UsuarioActual?.Rol != "Médico")
                ToolbarItems.Clear();
        }

        private void CargarMensajes()
        {
            Mensajes.Clear();
            var mensajesBD = Database.ObtenerConversacion(IdEmisor, IdReceptor);
            foreach (var m in mensajesBD)
                Mensajes.Add(m);

            Device.BeginInvokeOnMainThread(() =>
            {
                if (Mensajes.Count > 0)
                    MensajesListView.ScrollTo(Mensajes[Mensajes.Count - 1], ScrollToPosition.End, true);
            });
        }

        private void Enviar_Clicked(object sender, EventArgs e)
        {
            string texto = MensajeEntry.Text;

            if (!string.IsNullOrWhiteSpace(texto))
            {
                Database.GuardarMensaje(IdEmisor, IdReceptor, texto, false);
                MensajeEntry.Text = "";
                CargarMensajes();
            }
        }

        private async void Expediente_Clicked(object sender, EventArgs e)
        {
            var usuario = Database.GetUsuarioPorId(IdReceptor);
            if (usuario != null)
            {
                await Navigation.PushAsync(new HistorialPage(usuario.IdUsuario));
            }
            else
            {
                await DisplayAlert("Error", "No se pudo cargar el perfil del usuario.", "OK");
            }
        }
    }
}
