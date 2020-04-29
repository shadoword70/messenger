using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using ServerMessenger.Annotations;
using ServerMessenger.Classes;
using ServerMessenger.Comands;
using ServerMessenger.Helpers;
using ServerMessenger.Models;
using WcfService;

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
            get { return _create ?? (_create = new BaseButtonCommand((obj) =>
            {
                var logger = DIFactory.Resolve<ILogger>();
                IParameter[] data = {
                    new ConstructorArgument("logger", logger, false), 
                    new ConstructorArgument("ip", EndpointData.Ip, false),
                    new ConstructorArgument("port", EndpointData.Port.ToString(), false),
                    new ConstructorArgument("serverName", EndpointData.ServerName, false)
                };
                var serviceManager = (ServiceManager)DIFactory.Resolve<IServiceManager>(data);
                //ServiceBase.Run(serviceManager);
                serviceManager.StartService();
                _changeScreen();
            }, (obj) => !String.IsNullOrEmpty(EndpointData.Ip) && EndpointData.Port.HasValue && !String.IsNullOrEmpty(EndpointData.ServerName))); }
        }
    }
}
