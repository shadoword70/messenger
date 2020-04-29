using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using ClientMessenger.Properties;
using ClientMessenger.Views;

namespace ClientMessenger.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public delegate void ChangeScreen();

        public MainViewModel()
        {
            ChangeScreen changeScreen = ChangeScreenToRegistration;
            var connectModel = new ConnectViewModel(changeScreen);
            var connectControl = new ConnectControl(connectModel);
            Control = connectControl;
        }

        private UserControl _control;
        public UserControl Control
        {
            get
            {
                return _control;
            }

            set
            {
                _control = value;
                OnPropertyChanged("Control");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ChangeScreenToRegistration()
        {
            
            var viewModel = new RegistrationViewModel();
            var connectControl = new RegistrationControl(viewModel);
            Control = connectControl;
        }
    }
}
