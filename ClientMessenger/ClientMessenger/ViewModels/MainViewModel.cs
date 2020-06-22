using ClientMessenger.Properties;
using ClientMessenger.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

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
                OnPropertyChanged();
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
            var viewModel = new AuthorizationViewModel();
            var connectControl = new AuthorizationControl(viewModel);
            Control = connectControl;
        }
    }
}
