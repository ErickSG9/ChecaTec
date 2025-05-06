using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test
{
    public partial class PacientePage : ContentPage
    {
        public PacientePage(Usuario usuario)
        {
            InitializeComponent();
            BindingContext = usuario;
        }
    }
}