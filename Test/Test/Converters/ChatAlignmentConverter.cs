using System;
using System.Globalization;
using Xamarin.Forms;
using Test.Models;

namespace Test.Converters
{
    public class ChatAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var chat = value as Chat;
            if (chat == null || App.UsuarioActual == null)
                return LayoutOptions.Start;

            return chat.IdEmisor == App.UsuarioActual.IdUsuario
                ? LayoutOptions.End
                : LayoutOptions.Start;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
