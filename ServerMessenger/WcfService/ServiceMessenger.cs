using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Contracts;
using Common.Request;
using Common.Results;
using DbWorker;
using LoggerWorker;
using ServerHelper;

namespace WcfService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
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
            var callback = Callback;
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

            var queueUser = _users.SingleOrDefault(x => x.UserGuid == employee.Guid);
            if (queueUser == null)
            {
                var user = new User();
                user.UserGuid = employee.Guid;
                user.Callbacks.Add(callback);
                _users.Add(user);
            }
            else
            {
                queueUser.Callbacks.Add(callback);
            }

            var employees = await dbWorker.GetEmployees();
            var usersResult = employees.Select(dbEmployee => new Common.Results.User
            {
                Guid = dbEmployee.Guid,
                Email = dbEmployee.Email,
                Login = dbEmployee.Login,
                Surname = dbEmployee.Surname,
                Patronymic = dbEmployee.Patronymic,
                Position = dbEmployee.Position,
                Name = dbEmployee.Name,
                EmployeePhoto = dbEmployee.Photo,
            }).ToList();

            foreach (var user in _users)
            {
                try
                {
                    NeedUpdateChats(user.UserGuid);
                }
                catch (Exception e)
                {
                    _logger.Write(LogLevel.Error, "Error message: ", e);
                }
            }

            result.Employee = employee;
            result.Users = usersResult;
            result.InfoBody = new ResultBody { ResultStatus = ResultStatus.Success };
            
            return result;
        }

        private async void NeedUpdateChats(Guid employeeGuid)
        {
            var dbWorker = DIFactory.Resolve<IDbSystemWorker>();
            var employees = await dbWorker.GetEmployees();
            var usersResult = employees.Select(dbEmployee => new Common.Results.User
            {
                Guid = dbEmployee.Guid,
                Email = dbEmployee.Email,
                Login = dbEmployee.Login,
                Surname = dbEmployee.Surname,
                Patronymic = dbEmployee.Patronymic,
                Position = dbEmployee.Position,
                Name = dbEmployee.Name,
                EmployeePhoto = dbEmployee.Photo,
            }).ToList();

            foreach (var user in _users)
            {
                var single = usersResult.Single(x => x.Guid == user.UserGuid);
                single.IsOnline = user.Callbacks.Any();
            }

            var chats = await dbWorker.GetUserChats(employeeGuid);
            var needUpdateUsers = chats.Where(x => x.UserGuids.Count == 2).Select(x => x.UserGuids.First(y => y != employeeGuid)).Distinct().ToList();
            
            try
            {
                var selfUser = _users.SingleOrDefault(x => x.UserGuid == employeeGuid);
                var chatsResult = new UpdateChatsResult();
                chatsResult.Users = usersResult;
                chatsResult.Chats = chats;
                chatsResult.InfoBody = new ResultBody { ResultStatus = ResultStatus.Success };
                if (selfUser != null)
                {
                    foreach (var callback in selfUser.Callbacks)
                    {
                        try
                        {
                            callback.UpdateChats(chatsResult);
                        }
                        catch (Exception e)
                        {
                            _logger.Write(LogLevel.Error, "Error callback message", e);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Write(LogLevel.Error, "Error callback message", e);
            }

            foreach (var updateUser in needUpdateUsers)
            {
                var u = _users.SingleOrDefault(x => x.UserGuid == updateUser);
                if (u != null)
                {
                    foreach (var callback in u.Callbacks)
                    {
                        try
                        {
                            var chatsResult = new UpdateChatsResult();
                            chatsResult.Users = usersResult;
                            chatsResult.Chats = await dbWorker.GetUserChats(updateUser);
                            chatsResult.InfoBody = new ResultBody { ResultStatus = ResultStatus.Success };
                            callback.UpdateChats(chatsResult);
                        }
                        catch (Exception e)
                        {
                            _logger.Write(LogLevel.Error, "Error callback message", e);
                        }
                    }
                }
            }
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

                foreach (var user in _users)
                {
                    NeedUpdateChats(user.UserGuid);
                }

                return new ResultBody { ResultStatus = ResultStatus.Success };
            }
            catch (Exception e)
            {
                _logger.Write(LogLevel.Error, "Disconnect error", e);
                return new ResultBody { ResultStatus = ResultStatus.NotSuccess };
            }

        }

        public async void SendMessage(Guid selfGuid, Guid chatGuid, string message)
        {
            _logger.Write(LogLevel.Info, DateTime.Now + " " + chatGuid + " " + message);
            var worker = DIFactory.Resolve<IDbSystemWorker>();
            var date = DateTime.Now;
            await worker.SaveMessage(selfGuid, chatGuid, message, date);
            
            var updateChatUsers = await worker.GetUserFromChat(chatGuid);
            var updateUser = _users.Where(x => updateChatUsers.Contains(x.UserGuid));
            foreach (var user in updateUser)
            {
                foreach (var callback in user.Callbacks)
                {
                    try
                    {
                        callback.MessageCallback(date, chatGuid, message, selfGuid);
                    }
                    catch (Exception e)
                    {
                        _logger.Write(LogLevel.Error, "Error callback message", e);
                    }
                }
            }
        }

        public async void UpdatePhoto(Guid userGuid, byte[] photo)
        {
            var worker = DIFactory.Resolve<IDbSystemWorker>();
            await worker.UpdatePhoto(userGuid, Convert.ToBase64String(photo));
            
            foreach (var user in _users)
            {
                try
                {
                    NeedUpdateChats(user.UserGuid);
                }
                catch (Exception e)
                {
                    _logger.Write(LogLevel.Error, "Error callback message", e);
                }
            }
        }

        public async Task<GetChatResult> GetChat(Guid chatGuid)
        {
            var worker = DIFactory.Resolve<IDbSystemWorker>();
            var history = await worker.GetChat(chatGuid);
            return history;
        }

        public async void CreateGroupChat(CreateGroupChatRequest request)
        {
            var dbWorker = DIFactory.Resolve<IDbSystemWorker>();
            await dbWorker.CreateGroupChat(request.ChatName, request.CreatorGuid, request.UserGuids);

            foreach (var user in _users)
            {
                try
                {
                    NeedUpdateChats(user.UserGuid);
                }
                catch (Exception e)
                {
                    _logger.Write(LogLevel.Error, "Error callback message", e);
                }
            }

        }

        public async void CreateChat(Guid userGuid, Guid creatorGuid)
        {
            var dbWorker = DIFactory.Resolve<IDbSystemWorker>();
            await dbWorker.CreateChat(userGuid, creatorGuid);
            
            foreach (var user in _users)
            {
                try
                {
                    NeedUpdateChats(user.UserGuid);
                }
                catch (Exception e)
                {
                    _logger.Write(LogLevel.Error, "Error callback message", e);
                }
            }
        }

        public async Task<ResultBody> ChangePassword(Guid selfGuid, string oldPassword, string newPassword)
        {
            var dbWorker = DIFactory.Resolve<IDbSystemWorker>();
            var result = await dbWorker.ChangePassword(selfGuid, oldPassword, newPassword);
            return result;
        }

        IServiceMessengerCallback Callback => OperationContext.Current.GetCallbackChannel<IServiceMessengerCallback>();
    }
}
