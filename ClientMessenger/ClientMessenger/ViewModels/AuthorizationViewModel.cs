using ClientMessenger.Commands;
using ClientMessenger.Helpers;
using ClientMessenger.Models;
using ClientMessenger.Views;
using Common;
using ServiceWorker;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using ClientMessenger.Properties;

namespace ClientMessenger.ViewModels
{
    public class AuthorizationViewModel : INotifyPropertyChanged
    {
        private AuthorizationModel _authorizationData;
        public AuthorizationModel AuthorizationData
        {
            get { return _authorizationData; }
            set
            {
                _authorizationData = value;
                OnPropertyChanged("AuthorizationData");
            }
        }

        public string ClientName
        {
            get { return AuthorizationData.ClientName; }
            set
            {
                AuthorizationData.ClientName = value;
                OnPropertyChanged("ClientName");
            }
        }

        public string Password
        {
            get { return AuthorizationData.Password; }
            set
            {
                AuthorizationData.Password = value;
                OnPropertyChanged("Password");
            }
        }

        private bool _isVisibleLoading;
        public bool IsVisibleLoading
        {
            get { return _isVisibleLoading; }
            set
            {
                _isVisibleLoading = value;
                OnPropertyChanged("IsVisibleLoading");
            }
        }

        public AuthorizationViewModel()
        {
            _authorizationData = new AuthorizationModel();
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
                return _join ?? (_join = new BaseButtonCommand(async (obj) =>
                {
                    if (IsVisibleLoading)
                    {
                        return;
                    }
                    IsVisibleLoading = true;

                    try
                    {
                        var manager = DIFactory.Resolve<IServiceManager>();
                        var data = await manager.Authorization(AuthorizationData.ClientName, AuthorizationData.Password);
                        if (data.InfoBody.ResultStatus == ResultStatus.Success)
                        {
                            var callback = DIFactory.Resolve<IMessageCallback>();
                            var viewModel = new MessengerViewModel(callback, data);
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
                            MessageBox.Show(data.InfoBody.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    finally
                    {
                        IsVisibleLoading = false;
                    }

                }, (obj) => !String.IsNullOrEmpty(AuthorizationData.ClientName) && !String.IsNullOrEmpty(AuthorizationData.Password)));
            }
        }
    }
}
