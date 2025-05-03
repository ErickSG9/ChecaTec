using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Test.Data;
using Test.Models;

namespace Test
{
    public partial class MensajesPage : ContentPage
    {
        private int IdEmisor;
        private int IdReceptor;
        public MensajesPage()
        {
            InitializeComponent();
            CargarConversaciones();
        }

        private void CargarConversaciones()
        {
            var usuario = App.UsuarioActual;
            if (usuario == null)
                return;

            var chats = Database.GetChatsPorUsuario(usuario.IdUsuario);
            var usuariosConConversacion = chats
                .Select(c => c.IdEmisor == usuario.IdUsuario ? c.IdReceptor : c.IdEmisor)
                .Distinct()
                .ToList();

            var listaResumen = new List<ChatResumen>();

            foreach (var id in usuariosConConversacion)
            {
                var otroUsuario = Database.GetUsuarioPorId(id);
                if (otroUsuario == null) continue;

                var ultimoMensaje = chats
                    .Where(c => c.IdEmisor == otroUsuario.IdUsuario || c.IdReceptor == otroUsuario.IdUsuario)
                    .OrderByDescending(c => c.FechaEnvio)
                    .FirstOrDefault();

                listaResumen.Add(new ChatResumen
                {
                    IdEmisor = usuario.IdUsuario,
                    IdReceptor = otroUsuario.IdUsuario,
                    Nombre = otroUsuario.Nombre,
                    UltimoMensaje = ultimoMensaje?.Mensaje ?? "",
                    Hora = ultimoMensaje?.FechaEnvio.ToString("HH:mm"),
                    Foto = "perfil.png"
                });
            }


            MensajesList.ItemsSource = listaResumen;
        }
        private async void IrASeleccionarPaciente_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SeleccionarPacientePage());
        }
        private void MensajesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;

            var chatSeleccionado = e.CurrentSelection[0] as ChatResumen;

            if (chatSeleccionado == null)
                return;

            Navigation.PushAsync(new ChatPage(chatSeleccionado.IdEmisor, chatSeleccionado.IdReceptor));

            ((CollectionView)sender).SelectedItem = null;
        }



    }
}
