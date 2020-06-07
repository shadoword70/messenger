using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LoggerWorker;
using Ninject.Parameters;
using ServerHelper;
using ServerMessenger.Annotations;
using ServerMessenger.Classes;
using ServerMessenger.Commands;
using ServerMessenger.Helpers;
using ServerMessenger.Models;
using WcfService;
using WcfService.Workers;

namespace ServerMessenger.ViewModels
{
    public class EndpointViewModel : INotifyPropertyChanged
    {
        private EndpointModel _endpointData;
        public EndpointModel EndpointData
        {
            get { return _endpointData; }
            set
            {
                _endpointData = value;
                OnPropertyChanged("EndpointData");
            }
        }

        public NetworkInterface Endpoint
        {
            get { return EndpointData.Endpoint; }
            set
            {
                EndpointData.Endpoint = value;
                OnPropertyChanged("Endpoint");
            }
        }

        private ObservableCollection<NetworkInterface> _endpoints;
        public ObservableCollection<NetworkInterface> Endpoints
        {
            get { return _endpoints; }
            set
            {
                _endpoints = value;
                OnPropertyChanged("Endpoints");
            }
        }

        private MainViewModel.ChangeScreen _changeScreen;

        public EndpointViewModel(MainViewModel.ChangeScreen changeScreen)
        {
            _changeScreen = changeScreen;
            EndpointData = new EndpointModel();
            Endpoints = Network.GetNetworkInterfaces();
            if (Endpoints.Any())
            {
                Endpoint = Endpoints[0];
            }
            EndpointData.Port = "80";
            EndpointData.ServerName = "Messenger";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ICommand _create;
        public ICommand Create
        {
            get
            {
                return _create ?? (_create = new BaseButtonCommand((obj) =>
                {
                    var logger = DIFactory.Resolve<ILogger>();
                    IParameter[] data =
                    {
                        new ConstructorArgument("logger", logger, false),
                        new ConstructorArgument("ip", EndpointData.Ip, false),
                        new ConstructorArgument("port", EndpointData.Port, false),
                        new ConstructorArgument("serverName", EndpointData.ServerName, false),
                        new ConstructorArgument("systemWorker", DIFactory.Resolve<ISystemWorker>()), 
                    };
                    var serviceManager = DIFactory.Resolve<IServiceManager>(data);
                    serviceManager.StartService();
                    _changeScreen();
                }, (obj) =>CheckModel()));
            }
        }

        private bool CheckModel()
        {
            if (String.IsNullOrEmpty(EndpointData.Ip))
            {
                return false;
            }

            if (String.IsNullOrEmpty(EndpointData.Port))
            {
                return false;
            }

            int port;
            if (!int.TryParse(EndpointData.Port, out port))
            {
                return false;
            }

            if (port < 0 || port > 65536)
            {
                return false;
            }

            if (String.IsNullOrEmpty(EndpointData.ServerName))
            {
                return false;
            }

            if (!String.IsNullOrEmpty(EndpointData.Error))
            {
                return false;
            }

            return true;
        }
    }
}
