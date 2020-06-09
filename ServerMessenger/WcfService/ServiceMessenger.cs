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
            var currentContext = OperationContext.Current;
            AuthorizationResult result = new AuthorizationResult();
            
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

            _logger.Write(LogLevel.Info, DateTime.Now + " " + login + " Connect");

            var employees = await dbWorker.GetEmployees();
            var chats = await dbWorker.GetUserChats(employee.Guid);

            var queueUser = _users.SingleOrDefault(x => x.UserGuid == employee.Guid);
            if (queueUser == null)
            {
                var user = new User();
                user.UserGuid = employee.Guid;
                user.Contexts.Add(currentContext);
                _users.Add(user);
            }
            else
            {
                queueUser.Contexts.Add(currentContext);
            }

            //var usersResult = employees.DefaultIfEmpty().Join(_users.DefaultIfEmpty(),
            //    dbEmployee => dbEmployee.Guid,
            //    queueEmployee => queueEmployee.UserGuid,
            //    (dbEmployee, queueEmployee) => 
            //        new Common.Results.User
            //        {
            //            Guid = dbEmployee.Guid,
            //            Email = dbEmployee.Email,
            //            Login = dbEmployee.Login,
            //            Surname = dbEmployee.Surname,
            //            Patronymic = dbEmployee.Patronymic,
            //            Position = dbEmployee.Position,
            //            Name = dbEmployee.Name,
            //            IsOnline = queueEmployee.Contexts.Any()
            //        }).ToList();

            var usersResult = employees.Select(dbEmployee => new Common.Results.User
            {
                Guid = dbEmployee.Guid,
                Email = dbEmployee.Email,
                Login = dbEmployee.Login,
                Surname = dbEmployee.Surname,
                Patronymic = dbEmployee.Patronymic,
                Position = dbEmployee.Position,
                Name = dbEmployee.Name
            }).ToList();

            foreach (var user in _users)
            {
                var single = usersResult.Single(x => x.Guid == user.UserGuid);
                single.IsOnline = user.Contexts.Any();
            }

            var chatsResult = new UpdateChatsResult();
            chatsResult.Users = usersResult;
            chatsResult.Chats = chats;
            chatsResult.InfoBody = new ResultBody {ResultStatus = ResultStatus.Success};

            foreach (var u in _users)
            {
                try
                {
                    foreach (var context in u.Contexts)
                    {
                        context.GetCallbackChannel<IServiceMessengerCallback>().UpdateChats(chatsResult);
                    }
                }
                catch (Exception e)
                {
                    _logger.Write(LogLevel.Error, "Error callback message", e);
                }
            }

            result.Employee = employee;
            result.Users = usersResult;
            result.Chats = chats;
            result.InfoBody = new ResultBody { ResultStatus = ResultStatus.Success };
            
            return result;
        }

        public ResultBody DisconnectClient(Guid userGuid)
        {
            try
            {
                _logger.Write(LogLevel.Info, DateTime.Now + " " + userGuid + " Disconnect");
                var disUser = _users.SingleOrDefault(x => x.UserGuid == userGuid);
                if (disUser != null)
                {
                    _users.Remove(disUser);
                }

                return new ResultBody { ResultStatus = ResultStatus.Success };
            }
            catch (Exception e)
            {
                _logger.Write(LogLevel.Error, "Disconnect error", e);
                return new ResultBody { ResultStatus = ResultStatus.NotSuccess };
            }

        }

        public async void SendMessage(Guid selfGuid, Guid chatOrUserGuid, string message)
        {
            //var callback = Callback;
            _logger.Write(LogLevel.Info, DateTime.Now + " " + chatOrUserGuid + " " + message);
            var worker = DIFactory.Resolve<IDbSystemWorker>();
            var date = DateTime.Now;
            var chatGuid = await worker.SaveMessage(selfGuid, chatOrUserGuid, message, date);

            //callback.MessageCallback(date, chatGuid, message, selfGuid);

            foreach (var user in _users)
            {
                try
                {
                    var dbWorker = DIFactory.Resolve<IDbSystemWorker>();
                    var employees = await dbWorker.GetEmployees();
                    var chats = await dbWorker.GetUserChats(user.UserGuid);

                    var usersResult = employees.Select(dbEmployee => new Common.Results.User
                    {
                        Guid = dbEmployee.Guid,
                        Email = dbEmployee.Email,
                        Login = dbEmployee.Login,
                        Surname = dbEmployee.Surname,
                        Patronymic = dbEmployee.Patronymic,
                        Position = dbEmployee.Position,
                        Name = dbEmployee.Name
                    }).ToList();

                    foreach (var u in _users)
                    {
                        var single = usersResult.Single(x => x.Guid == u.UserGuid);
                        single.IsOnline = user.Contexts.Any();
                    }

                    var chatsResult = new UpdateChatsResult();
                    chatsResult.Users = usersResult;
                    chatsResult.Chats = chats;
                    chatsResult.InfoBody = new ResultBody {ResultStatus = ResultStatus.Success};

                    foreach (var u in _users)
                    {
                        try
                        {
                            foreach (var context in u.Contexts)
                            {
                                context.GetCallbackChannel<IServiceMessengerCallback>().UpdateChats(chatsResult);
                            }
                        }
                        catch (Exception e)
                        {
                            _logger.Write(LogLevel.Error, "Error callback message", e);
                        }
                    }

                    foreach (var context in user.Contexts)
                    {
                        context.GetCallbackChannel<IServiceMessengerCallback>().MessageCallback(date, chatGuid, message, selfGuid);
                    }
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
