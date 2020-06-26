using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using ClientMessenger.Models;
using ClientMessenger.Views;

namespace ClientMessenger.Converters
{
    public class PasswordConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.Length == 3)
            {
                PasswordElements elements = new PasswordElements();
                elements.OldPassword = values[0] as PasswordBox;
                elements.NewPassword = values[1] as PasswordBox;
                elements.RepeatNewPassword = values[2] as PasswordBox;
                return elements;
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
