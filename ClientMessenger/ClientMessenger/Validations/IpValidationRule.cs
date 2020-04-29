using System.Globalization;
using System.Net;
using System.Windows.Controls;

namespace ClientMessenger.Validations
{
    public class IpValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            IPAddress ip;
            if (value != null && IPAddress.TryParse(value.ToString(), out ip))
            {
                return new ValidationResult(true, ip);
            }

            return new ValidationResult(false, null);
        }
    }
}
