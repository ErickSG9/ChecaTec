using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Test.Models; 
using Test.Data;   
namespace Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsuariosPage : ContentPage
    {
        public UsuariosPage()
        {
            InitializeComponent();
        }
        public UsuariosPage(object parametro) 
        {
            InitializeComponent();
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            var usuarios = Database.GetConnection().Table<Usuario>().ToList();
            usuariosListView.ItemsSource = usuarios;
        }
    }
}
