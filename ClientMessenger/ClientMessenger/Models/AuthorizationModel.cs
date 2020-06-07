using System.ComponentModel;
using System.Runtime.CompilerServices;
using ClientMessenger.Properties;

namespace ClientMessenger.Models
{
    public class AuthorizationModel : INotifyPropertyChanged
    {
        private string _clientName;
        public string ClientName
        {
            get { return _clientName; }
            set
            {
                _clientName = value;
                OnPropertyChanged("ClientName");
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
