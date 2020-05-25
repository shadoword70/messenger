using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using LoggerWorker;
using ServerMessenger.Helpers;

namespace ServerMessenger.Classes
{
    public static class Network
    {
        public static ObservableCollection<NetworkInterface> GetNetworkInterfaces()
        {
            var logger = DIFactory.Resolve<ILogger>();

            try
            {
                IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
                List<NetworkInterface> nics = NetworkInterface.GetAllNetworkInterfaces().Where(x => 
                (x.NetworkInterfaceType == NetworkInterfaceType.Ethernet 
                || x.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                && x.Supports(NetworkInterfaceComponent.IPv4)
                && x.OperationalStatus == OperationalStatus.Up).ToList();
                
                logger.Write(LogLevel.Info, "Interface information for " + computerProperties.HostName + "." + computerProperties.DomainName);
                if (!nics.Any())
                {
                    logger.Write(LogLevel.Warning, "No network interfaces found.");
                    return null;
                }

                var endpoints = new ObservableCollection<NetworkInterface>();
                foreach (NetworkInterface networkInterface in nics)
                {
                    endpoints.Add(networkInterface);
                }
                return endpoints;
            }
            catch (Exception e)
            {
                logger.Write(LogLevel.Error, "", e);
                throw;
            }
        }
    }
}
