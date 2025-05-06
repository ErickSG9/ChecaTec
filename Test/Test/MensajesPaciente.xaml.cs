using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Test.Views;
using Test.Data;
using Test.Models;

namespace Test
{
    public partial class MensajesPaciente : ContentPage
    {
        private List<ChatResumen> chatsOriginales;

        public MensajesPaciente()
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

            chatsOriginales = new List<ChatResumen>();

            foreach (var id in usuariosConConversacion)
            {
                var otroUsuario = Database.GetUsuarioPorId(id);
                if (otroUsuario == null) continue;

                var ultimoMensaje = chats
                    .Where(c => c.IdEmisor == otroUsuario.IdUsuario || c.IdReceptor == otroUsuario.IdUsuario)
                    .OrderByDescending(c => c.FechaEnvio)
                    .FirstOrDefault();

                chatsOriginales.Add(new ChatResumen
                {
                    IdEmisor = usuario.IdUsuario,
                    IdReceptor = otroUsuario.IdUsuario,
                    Nombre = otroUsuario.Nombre,
                    UltimoMensaje = ultimoMensaje?.Mensaje ?? "",
                    Hora = ultimoMensaje?.FechaEnvio.ToString("HH:mm"),
                    Foto = "perfil.png"
                });
            }

            MensajesList.ItemsSource = chatsOriginales;
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = e.NewTextValue?.ToLower() ?? "";

            // Si la búsqueda está vacía, mostramos todo
            if (string.IsNullOrWhiteSpace(filtro))
            {
                MensajesList.ItemsSource = chatsOriginales;
                return;
            }

            // Buscar coincidencias flexibles
            var coincidencias = chatsOriginales.Where(c =>
                c.Nombre.ToLower().Contains(filtro) ||           // Contiene en cualquier parte
                c.Nombre.ToLower().StartsWith(filtro) ||         // Empieza con...
                (c.Nombre.ToLower().Split(' ').Any(p => p.StartsWith(filtro))) // Por partes del nombre
            ).ToList();

            MensajesList.ItemsSource = coincidencias;
        }
        private void OnSearchButtonPressed(object sender, EventArgs e)
        {
            var searchBar = sender as SearchBar;
            string filtro = searchBar.Text?.ToLower() ?? "";

            var coincidencias = chatsOriginales.Where(c =>
                c.Nombre.ToLower().Contains(filtro) ||
                c.Nombre.ToLower().StartsWith(filtro) ||
                (c.Nombre.ToLower().Split(' ').Any(p => p.StartsWith(filtro)))
            ).ToList();

            MensajesList.ItemsSource = coincidencias;
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

        private async void VerPerfil_Clicked(object sender, EventArgs e)
        {
            var boton = sender as Button;
            if (boton?.CommandParameter is ChatResumen chat)
            {
                var receptor = Database.GetUsuarioPorId(chat.IdReceptor);
                if (receptor != null)
                {
                    await Navigation.PushAsync(new PacientePage(receptor));
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo cargar el perfil del usuario.", "OK");
                }
            }
        }

    }
}


