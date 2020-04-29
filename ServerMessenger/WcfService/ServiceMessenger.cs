using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Contracts;
using LoggerWorker;

namespace WcfService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceMessenger : IServiceMessenger
    {
        private ILogger _logger;
        private List<User> _users;

        public ServiceMessenger(ILogger logger)
        {
            _logger = logger;
            _users = new List<User>();
        }

        public ResultBody RegistrationClient(string name)
        {
            _logger.Write(LogLevel.Info, DateTime.Now + " " + name + " Connect");
            if (String.IsNullOrEmpty(name))
            {
                return new ResultBody {ResultStatus = ResultStatus.NotSuccess};
            }

            if (_users.Exists(x => x.Name == name))
            {
                return new ResultBody { ResultStatus = ResultStatus.NotSuccess };
            }

            var user = new User();
            user.Name = name;
            user.Context = OperationContext.Current;
            SendMessage(user.Name + " Connect chat", String.Empty);
            _users.Add(user);
            var names = _users.Select(x => x.Name).ToList();
            foreach (var u in _users)
            {
                try
                {
                    if (u.Name == name)
                    {
                        continue;
                    }

                    u?.Context.GetCallbackChannel<IServiceMessengerCallback>().UpdateUsers(names);
                }
                catch (Exception e)
                {
                    _logger.Write(LogLevel.Error, "Error callback message", e);
                    continue;
                }
            }

            return new ResultBody { ResultData = names, ResultStatus = ResultStatus.Success };
        }

        public ResultBody DisconnectClient(string name)
        {
            try
            {
                _logger.Write(LogLevel.Info, DateTime.Now + " " + name + " Disconnect");
                var disUser = _users.SingleOrDefault(x => x.Name == name);
                if (disUser != null)
                {
                    _users.Remove(disUser);
                    SendMessage(disUser.Name + "Disconnect chat", String.Empty);
                }

                return new ResultBody { ResultStatus = ResultStatus.Success };
            }
            catch (Exception e)
            {
                _logger.Write(LogLevel.Error, "Disconnect error", e);
                return new ResultBody { ResultStatus = ResultStatus.NotSuccess };
            }

        }

        public void SendMessage(string name, string message)
        {
            _logger.Write(LogLevel.Info, DateTime.Now + " " + name + " " + message);
            var date = DateTime.Now;
            var user = _users.SingleOrDefault(x => x.Name == name);
            if (user != null)
            {
                try
                {
                    user?.Context.GetCallbackChannel<IServiceMessengerCallback>().MessageCallback(date, user.Name, message);
                    Callback.MessageCallback(date, user.Name, message);
                }
                catch (Exception e)
                {
                    _logger.Write(LogLevel.Error, "Error callback message", e);
                }
            }
        }

        IServiceMessengerCallback Callback
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<IServiceMessengerCallback>();
            }
        }
    }
}
