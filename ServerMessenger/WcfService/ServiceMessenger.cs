using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Contracts;
using Common.Results;
using DbWorker;
using LoggerWorker;
using ServerHelper;

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

        public async Task<AuthorizationResult> AuthorizationClient(string login, string password)
        {
            AuthorizationResult result = new AuthorizationResult();
            
            _logger.Write(LogLevel.Info, DateTime.Now + " " + login + " Connect");
            if (String.IsNullOrEmpty(login))
            {
                result.InfoBody = new ResultBody
                {
                    ResultStatus = ResultStatus.NotSuccess,
                    Message = "Login empty!"
                };
                return result;
            }

            if (String.IsNullOrEmpty(password))
            {
                result.InfoBody = new ResultBody
                {
                    ResultStatus = ResultStatus.NotSuccess,
                    Message = "Password empty!"
                };
                return result;
            }

            var dbWorker = DIFactory.Resolve<IDbSystemWorker>();
            var employee = await dbWorker.GetEmployee(login, password);
            if (employee == null)
            {
                result.InfoBody = new ResultBody
                {
                    ResultStatus = ResultStatus.NotSuccess,
                    Message = "Пользователь с таким логином и паролем не найден!"
                };
                return result;
            }

            var user = new User();
            user.Login = login;
            user.Context = OperationContext.Current;
            SendMessage(user.Login + " Connect chat", String.Empty);
            _users.Add(user);
            var names = _users.Select(x => x.Login).ToList();
            foreach (var u in _users)
            {
                try
                {
                    if (u.Login == login)
                    {
                        continue;
                    }

                    u.Context.GetCallbackChannel<IServiceMessengerCallback>().UpdateUsers(names);
                }
                catch (Exception e)
                {
                    _logger.Write(LogLevel.Error, "Error callback message", e);
                }
            }

            result.Employee = employee;
            result.Users = names;
            result.InfoBody = new ResultBody { ResultStatus = ResultStatus.Success };
            
            return result;
        }

        public ResultBody DisconnectClient(string name)
        {
            try
            {
                _logger.Write(LogLevel.Info, DateTime.Now + " " + name + " Disconnect");
                var disUser = _users.SingleOrDefault(x => x.Login == name);
                if (disUser != null)
                {
                    _users.Remove(disUser);
                    SendMessage(disUser.Login + "Disconnect chat", String.Empty);
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
            var user = _users.SingleOrDefault(x => x.Login == name);
            if (user != null)
            {
                try
                {
                    user.Context.GetCallbackChannel<IServiceMessengerCallback>().MessageCallback(date, user.Login, message);
                    Callback.MessageCallback(date, user.Login, message);
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
