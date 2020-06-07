using System;
using System.Globalization;
using System.Windows.Controls;

namespace ClientMessenger.Validations
{
    public class PortValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int port;
            if (value != null && Int32.TryParse(value.ToString(), out port) && port >= 0 && port <= 65535)
            {
                return new ValidationResult(true, port);
            }

            return new ValidationResult(false, null);
        }
    }
}
