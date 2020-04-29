﻿using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ClientMessenger.Commands;
using ClientMessenger.Helpers;
using ClientMessenger.Models;
using ClientMessenger.Properties;
using Ninject.Parameters;
using ServiceWorker;

namespace ClientMessenger.ViewModels
{
    public class ConnectViewModel : INotifyPropertyChanged
    {
        private ConnectModel _connectData;

        public ConnectModel ConnectData
        {
            get { return _connectData; }
            set
            {
                _connectData = value;
                OnPropertyChanged("ConnectData");
            }
        }

        private MainViewModel.ChangeScreen _changeScreen;

        public ConnectViewModel(MainViewModel.ChangeScreen changeScreen)
        {
            _changeScreen = changeScreen;
            ConnectData = new ConnectModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ICommand _connect;
        public ICommand Connect
        {
            get
            {
                return _connect ?? (_connect = new BaseButtonCommand((obj) =>
                {
                    var callback = DIFactory.Resolve<IMessageCallback>();
                    IParameter[] parameters = new Parameter[]
                    {
                        new ConstructorArgument("ip", ConnectData.Ip, false),
                        new ConstructorArgument("port", ConnectData.Port, false),
                        new ConstructorArgument("callback", callback, false)
                    };
                    DIFactory.Resolve<IServiceManager>(parameters);
                    _changeScreen();
                }, (obj) => CanExecuteConnect()));
            }
        }

        private bool CanExecuteConnect()
        {
            var ipData = ConnectData.Ip;
            IPAddress ip;
            if (String.IsNullOrEmpty(ipData) || !IPAddress.TryParse(ipData, out ip))
            {
                return false;
            }


            var portData = ConnectData.Port;
            int port;
            if (String.IsNullOrEmpty(portData) || !Int32.TryParse(portData, out port))
            {
                return false;
            }

            if (port < 0 && port > 65535)
            {
                return false;
            }

            return true;
        }
    }
}
