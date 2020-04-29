using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ServerMessenger.Annotations;

namespace ServerMessenger.Models
{
    public class EndpointModel : INotifyPropertyChanged
    {
        private int? _port;
        public int? Port
        {
            get { return _port; }
            set
            {
                _port = value;
                OnPropertyChanged("Port");
            }
        }

        private string _serverName;
        public string ServerName
        {
            get { return _serverName; }
            set
            {
                _serverName = value;
                OnPropertyChanged("ServerName");
            }
        }

        private NetworkInterface _endpoint;
        public NetworkInterface Endpoint
        {
            get { return _endpoint; }
            set
            {
                _endpoint = value;
                OnPropertyChanged("Endpoint");
                OnPropertyChanged("Ip");
            }
        }

        public string Ip
        {
            get
            {
                if (Endpoint == null)
                {
                    return String.Empty;
                }

                var addresses = Endpoint.GetIPProperties().UnicastAddresses
                    .FirstOrDefault(x => x.Address.AddressFamily == AddressFamily.InterNetwork);
                if (addresses != null)
                {
                    return addresses.Address.ToString();
                }

                return String.Empty;
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
