using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Test.Models;
using Test.Data;

namespace Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeleccionarPacientePage : ContentPage
    {
        public SeleccionarPacientePage()
        {
            InitializeComponent();
            CargarPacientes();
        }

        private void CargarPacientes()
        {
            var usuarioActual = App.UsuarioActual;
            var chats = Database.GetChatsPorUsuario(usuarioActual.IdUsuario);
            var usuariosYaChateados = chats
                .Select(c => c.IdEmisor == usuarioActual.IdUsuario ? c.IdReceptor : c.IdEmisor)
                .Distinct()
                .ToList();

            var pacientes = Database.GetUsuariosPorRol("Paciente");
            var disponibles = pacientes.Where(p => !usuariosYaChateados.Contains(p.IdUsuario)).ToList();

            UsuariosList.ItemsSource = disponibles;
        }

        private async void UsuariosList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Usuario seleccionado)
            {
                // Crear primer mensaje o redirigir al chat (si implementado)
                Database.GuardarMensaje(App.UsuarioActual.IdUsuario, seleccionado.IdUsuario, "Hola, ¿en qué puedo ayudarte?", false);
                await DisplayAlert("Conversación iniciada", $"Con {seleccionado.Nombre}", "OK");
                await Navigation.PopAsync(); // Volver a la pantalla anterior
            }
        }
    }
}
