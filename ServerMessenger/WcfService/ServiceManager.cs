using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
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
        private string _ip;
        private string _port;
        private string _serverName;

        public ServiceManager(ILogger logger, string ip, string port, string serverName)
        {
            _logger = logger;
            _ip = ip;
            _port = port;
            _serverName = serverName;
        }

        private void CreateHost()
        {
            try
            {
                Uri address = new Uri($"net.tcp://{_ip}:{_port}/IServiceMessenger");

                NetTcpBinding binding = new NetTcpBinding();
                binding.MaxReceivedMessageSize = Int32.MaxValue;
                Type concract = typeof(IServiceMessenger);

                var messenger = new ServiceMessenger(_logger);
                _host = new ServiceHost(messenger);
                _host.AddServiceEndpoint(concract, binding, address);
                _host.Opened += HostOnOpened;
                _host.Closed += HostOnClosed;
                _host.Faulted += HostOnFaulted;
                _host.UnknownMessageReceived += HostOnUnknownMessageReceived;

            }
            catch (Exception e)
            {
                _logger.Write(LogLevel.Error, "CreateHost: ", e);
            }
        }

        private void HostOnUnknownMessageReceived(object sender, UnknownMessageReceivedEventArgs e)
        {
            _logger.Write(LogLevel.Error, sender.ToString() + ": " + e.Message);
        }

        private void HostOnFaulted(object sender, EventArgs e)
        {
            _logger.Write(LogLevel.Error, sender.ToString() + ": Communication faulted");
        }

        private void HostOnClosed(object sender, EventArgs e)
        {
            _logger.Write(LogLevel.Info, sender.ToString() + ": Communication closed");
        }

        private void HostOnOpened(object sender, EventArgs e)
        {
            _logger.Write(LogLevel.Info, sender.ToString() + ": Communication opened");
        }

        public void StartService()
        {
            try
            {
                _logger.Write(LogLevel.Info, "Server start");
                _host?.Close();

                CreateHost();
                _host?.Open();
            }
            catch(Exception ex)
            {
                _logger.Write(LogLevel.Error, "Не удалось создать хостинг", ex);
                StopService();
            }
            
        }

        public void StopService()
        {
            try
            {
                _logger.Write(LogLevel.Info, "Server stop");
                _host?.Close();
                _host = null;
            }
            catch (Exception ex)
            {
                _logger.Write(LogLevel.Error, "", ex);
                StopService();
            }
        }
    }
}
