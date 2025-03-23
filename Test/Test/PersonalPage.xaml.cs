using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Test
{
    public partial class PersonalPage : ContentPage
    {
        public class Doctor
        {
            public string Foto { get; set; }
            public string Nombre { get; set; }
            public string Especialidad { get; set; }
        }

        public PersonalPage()
        {
            InitializeComponent();
            CargarDoctores();
        }

        void CargarDoctores()
        {
            var lista = new List<Doctor>
            {
                new Doctor { Foto = "doctor1.png", Nombre = "Dr. Juan Ramírez", Especialidad = "Cardiólogo" },
                new Doctor { Foto = "doctor2.png", Nombre = "Dra. Laura Gómez", Especialidad = "Pediatra" },
                new Doctor { Foto = "doctor3.png", Nombre = "Dr. Luis Torres", Especialidad = "Internista" }
            };

            DoctoresList.ItemsSource = lista;
        }

        async void VerDoctor_Clicked(object sender, EventArgs e)
        {
            var boton = sender as Button;
            var doctor = boton?.BindingContext as Doctor;

            if (doctor != null)
            {
                await DisplayAlert(doctor.Nombre, $"Especialidad: {doctor.Especialidad}", "Cerrar");
            }
        }
    }
}