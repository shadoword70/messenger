using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ClientMessenger.Annotations;
using ClientMessenger.Commands;
using ClientMessenger.Helpers;
using ClientMessenger.Models;
using ClientMessenger.Views;
using Common;
using Common.Contracts;
using Ninject.Parameters;
using ServiceWorker;

namespace ClientMessenger.ViewModels
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        private RegistrationModel _registrationData;
        public RegistrationModel RegistrationData
        {
            get { return _registrationData; }
            set
            {
                _registrationData = value;
                OnPropertyChanged("RegistrationData");
            }
        }

        public string ClientName
        {
            get { return RegistrationData.ClientName; }
            set
            {
                RegistrationData.ClientName = value;
                OnPropertyChanged("ClientName");
            }
        }


        public RegistrationViewModel()
        {
            _registrationData = new RegistrationModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ICommand _join;

        public ICommand Join
        {
            get
            {
                return _join ?? (_join = new BaseButtonCommand((obj) =>
                {
                    var manager = DIFactory.Resolve<IServiceManager>();
                    var data = manager.Registration(RegistrationData.ClientName);
                    if (data.ResultStatus == ResultStatus.Success)
                    {
                        var callback = DIFactory.Resolve<IMessageCallback>();
                        var viewModel = new MessengerViewModel(RegistrationData.ClientName, callback, data.ResultData);
                        MessengerWindow window = new MessengerWindow(viewModel);

                        App.Current.MainWindow.Hide();
                        if (window.ShowDialog() == true)
                        {

                        }
                        else
                        {
                            App.Current.MainWindow.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error", "Result not success!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }, (obj) => !String.IsNullOrEmpty(RegistrationData.ClientName)));
            }
        }
    }
}
