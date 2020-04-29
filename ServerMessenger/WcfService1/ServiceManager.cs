using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using Common.Contracts;
using LoggerWorker;

namespace WcfService
{
    public class ServiceManager : IServiceManager
    {
        private ServiceHost _host;
        private ILogger _logger;

        public ServiceManager(ILogger logger, string ip, string port)
        {
            _logger = logger;
            try
            {
                Uri address = new Uri($"net.tcp://{ip}:{port}/IServiceMessenger");

                NetTcpBinding binding = new NetTcpBinding();
                Type concract = typeof(IServiceMessenger);

                _host = new ServiceHost(typeof(ServiceMessenger));
                _host.AddServiceEndpoint(concract, binding, address);
                _host.Opened += HostOnOpened;
                _host.Closed += HostOnClosed;
                _host.Faulted += HostOnFaulted;
                _host.UnknownMessageReceived += HostOnUnknownMessageReceived;

            }
            catch (Exception e)
            {
                _logger.Write(LogLevel.Error, "Не удалось создать хостинг", e);
                throw;
            }
        }

        private void HostOnUnknownMessageReceived(object sender, UnknownMessageReceivedEventArgs e)
        {
            _logger.Write(LogLevel.Error, sender.ToString() + Environment.NewLine + e.Message);
        }

        private void HostOnFaulted(object sender, EventArgs e)
        {
            _logger.Write(LogLevel.Error, sender.ToString() + Environment.NewLine + "Faulted");
        }

        private void HostOnClosed(object sender, EventArgs e)
        {
            _logger.Write(LogLevel.Info, sender.ToString() + Environment.NewLine + "Server closed");
        }

        private void HostOnOpened(object sender, EventArgs e)
        {
            _logger.Write(LogLevel.Info, sender.ToString() + Environment.NewLine + "Server opened");
        }

        public void StartService()
        {
            _host?.Open();
        }

        public void StopService()
        {
            _host.Close();
        }
    }
}
