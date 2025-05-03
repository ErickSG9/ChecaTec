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

            CargarMensajes();
        }
        private void CargarMensajes()
        {
            var mensajes = Database.ObtenerConversacion(IdEmisor, IdReceptor);
            MensajesListView.ItemsSource = mensajes;
        }
        private void Enviar_Clicked(object sender, EventArgs e)
        {
            string texto = MensajeEntry.Text;

            if (!string.IsNullOrWhiteSpace(texto))
            {
                Database.GuardarMensaje(App.UsuarioActual.IdUsuario, receptor.IdUsuario, texto, false);
                MensajeEntry.Text = "";
                CargarMensajes();
            }
        }
    }
}
