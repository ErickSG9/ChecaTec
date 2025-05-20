using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Test.Views;
using Test.Data;
using Test.Models;

namespace Test
{
    public partial class MensajesPage : ContentPage
    {
        private List<ChatResumen> chatsOriginales;

        public MensajesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CargarConversaciones();
        }

        private async void CerrarSesion_Clicked(object sender, EventArgs e)
        {
            bool respuesta = await DisplayAlert("Cerrar sesión", "¿Está seguro de que desea cerrar sesión?", "Sí", "Cancelar");
            if (respuesta)
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }

        private void CargarConversaciones()
        {
            var usuario = App.UsuarioActual;
            if (usuario == null)
                return;

            var chats = Database.GetChatsPorUsuario(usuario.IdUsuario);

            // Verifica que haya chats antes de procesar
            if (chats == null || chats.Count == 0)
            {
                chatsOriginales = new List<ChatResumen>();
                MensajesList.ItemsSource = chatsOriginales;
                return;
            }

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
                    FechaHora = ultimoMensaje?.FechaEnvio ?? DateTime.MinValue,
                    Foto = "perfil.png"
                });
            }

            chatsOriginales = chatsOriginales
                .OrderByDescending(c => c.FechaHora)
                .ToList();

            MensajesList.ItemsSource = chatsOriginales;
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = e.NewTextValue?.ToLower() ?? "";

            if (string.IsNullOrWhiteSpace(filtro))
            {
                MensajesList.ItemsSource = chatsOriginales;
                return;
            }

            var coincidencias = chatsOriginales.Where(c =>
                c.Nombre.ToLower().StartsWith(filtro)
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
                c.Nombre.ToLower().Split(' ').Any(p => p.StartsWith(filtro))
            ).ToList();

            MensajesList.ItemsSource = coincidencias;
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

        private async void VerPerfil_Clicked(object sender, EventArgs e)
        {
            var boton = sender as Button;
            if (boton?.CommandParameter is ChatResumen chat)
            {
                var receptor = Database.GetUsuarioPorId(chat.IdReceptor);
                if (receptor != null)
                {
                    await Navigation.PushAsync(new HistorialPage(receptor.IdUsuario));
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo cargar el perfil del usuario.", "OK");
                }
            }
        }
    }
}


