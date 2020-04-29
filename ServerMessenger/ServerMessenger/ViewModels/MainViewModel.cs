using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ServerMessenger.Annotations;
using ServerMessenger.Views;

namespace ServerMessenger.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public delegate void ChangeScreen();
        public MainViewModel()
        {
            ChangeScreenToCreateServer();
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

        private void ChangeScreenToStartServer()
        {
            ChangeScreen changeScreen = ChangeScreenToCreateServer;
            var viewModel = new ViewLogViewModel(changeScreen);
            var control = new ViewLogControl(viewModel);
            Control = control;
        }

        private void ChangeScreenToCreateServer()
        {
            ChangeScreen changeScreen = ChangeScreenToStartServer;
            var endpointViewModel = new EndpointViewModel(changeScreen);
            var connectControl = new EndpointControl(endpointViewModel);
            Control = connectControl;
        }
    }
}
