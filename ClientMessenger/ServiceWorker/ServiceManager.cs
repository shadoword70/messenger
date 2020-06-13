using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Common;
using Common.Contracts;
using Common.Results;

namespace ServiceWorker
{
    public class ServiceManager : IServiceManager
    {
        public event EventHandler Disconnected;
        private Timer _timer;
        private DateTime _lastConnect;
        private IServiceMessenger _channel;
        private string _ip;
        private string _port;
        private IMessageCallback _callback;


        public ServiceManager(string ip, string port, IMessageCallback callback)
        {
            _ip = ip;
            _port = port;
            _callback = callback;

            InitiationChannel(_ip, _port, _callback);
        }

        private void InitiationChannel(string ip, string port, IMessageCallback callback)
        {
            try
            {
                Uri address = new Uri($"net.tcp://{ip}:{port}/IServiceMessenger");

                NetTcpBinding binding = new NetTcpBinding();
                binding.MaxReceivedMessageSize = Int32.MaxValue;
                EndpointAddress endpoint = new EndpointAddress(address);
                InstanceContext instanceContext = new InstanceContext(callback);
                DuplexChannelFactory<IServiceMessenger> factory =
                    new DuplexChannelFactory<IServiceMessenger>(instanceContext, binding, endpoint);
                _channel = factory.CreateChannel();
                _timer = new Timer(10000);
                _timer.Enabled = true;
                _timer.AutoReset = true;
                _timer.Elapsed += TimerOnElapsed;
                _lastConnect = DateTime.Now;
            }
            catch (Exception ex)
            {

            }
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

        public async Task<AuthorizationResult> Authorization(string login, string password)
        {
            try
            {
                _lastConnect = DateTime.Now;

                return await _channel.AuthorizationClient(login, password);
            }
            catch (Exception ex)
            {
                InitiationChannel(_ip, _port, _callback);
                return new AuthorizationResult { InfoBody = new ResultBody {ResultStatus = ResultStatus.NotSuccess, Message = ex.Message}};
            }
        }

        public ResultBody Disconnect(Guid userGuid)
        {
            try
            {
                return _channel.DisconnectClient(userGuid);
            }
            catch
            {
                return new ResultBody { ResultStatus = ResultStatus.NotSuccess };
            }
        }

        public void SendMessage(Guid selfGuid, Guid chatOrUserGuid, string message)
        {
            try
            {
                _lastConnect = DateTime.Now;
                _channel.SendMessage(selfGuid, chatOrUserGuid, message);
            }
            catch
            {

            }
        }

        public void UpdatePhoto(Guid userGuid, byte[] photo)
        {
            try
            {
                _channel.UpdatePhoto(userGuid, photo);
            }
            catch (Exception ex)
            {
            }
        }

        public async Task<GetChatResult> GetChat(Guid chatGuid)
        {
            return await _channel.GetChat(chatGuid);
        }
    }
}
