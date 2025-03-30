﻿using System;
using System.Collections.Generic;
using Test.Data;
using Xamarin.Forms;

namespace Test.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void IniciarSesion_Clicked(object sender, System.EventArgs e)
        {
            string email = entryUsuario.Text;
            string clave = entryContrasena.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(clave))
            {
                await DisplayAlert("Error", "Ingrese correo y contraseña", "OK");
                return;
            }

            var usuario = Database.ValidarUsuario(email, clave);

            if (usuario != null)
            {
                await DisplayAlert("Bienvenido", $"Acceso correcto, {usuario.Nombre}", "Continuar");

                // Si es médico, ir a MainPage con acceso médico
                if (usuario.Rol == "Médico")
                {
                    Application.Current.MainPage = new NavigationPage(new MainPage());
                }
                // Si es paciente, ir a MainPage con acceso paciente
                else if (usuario.Rol == "Paciente")
                {
                    Application.Current.MainPage = new NavigationPage(new MainPage());
                }
            }
            else
            {
                await DisplayAlert("Error", "Correo o contraseña incorrectos", "Intentar de nuevo");
            }
        }

        private async void IrARegistro_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistroPage());
        }
        private void MostrarOcultarContrasena_Clicked(object sender, EventArgs e)
        {
            entryContrasena.IsPassword = !entryContrasena.IsPassword;
        }
    }
}
