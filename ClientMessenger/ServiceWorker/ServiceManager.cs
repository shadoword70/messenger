using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Common;
using Common.Contracts;

namespace ServiceWorker
{
    public class ServiceManager : IServiceManager
    {
        public event EventHandler Disconnected;
        private Timer _timer;
        private DateTime _lastConnect;
        private IServiceMessenger _channel;
        public ServiceManager(string ip, string port, ServiceMessengerCallback callback)
        {
            Uri address = new Uri($"net.tcp://{ip}:{port}/IServiceMessenger");

            NetTcpBinding binding = new NetTcpBinding();

            EndpointAddress endpoint = new EndpointAddress(address);
            InstanceContext instanceContext = new InstanceContext(callback);
            
            DuplexChannelFactory <IServiceMessenger> factory = new DuplexChannelFactory<IServiceMessenger>(instanceContext, binding, endpoint);
            _channel = factory.CreateChannel();
            _timer = new Timer(10000);
            _timer.Enabled = true;
            _timer.AutoReset = true;
            _timer.Elapsed += TimerOnElapsed;
            _lastConnect = DateTime.Now;
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            var overDate = _lastConnect.AddMinutes(20);
            if (DateTime.Now > overDate)
            {
                Disconnected?.Invoke(sender, e);
                _timer?.Dispose();
                _timer = null;
            }
        }

        public ResultBody Registration(string name)
        {
            try
            {
                _lastConnect = DateTime.Now;
                return _channel.RegistrationClient(name);
            }
            catch
            {
                return new ResultBody {ResultStatus = ResultStatus.NotSuccess};
            }
        }

        public ResultBody Disconnect(string name)
        {
            try
            {
                return _channel.DisconnectClient(name);
            }
            catch
            {
                return new ResultBody { ResultStatus = ResultStatus.NotSuccess };
            }
        }

        public void SendMessage(string name, string message)
        {
            try
            {
                _lastConnect = DateTime.Now;
                _channel.SendMessage(name, message);
            }
            catch
            {
            }
        }
    }
}
