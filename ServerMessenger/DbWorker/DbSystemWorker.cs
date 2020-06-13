using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations.History;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Db = DbWorker.DbElements;
using System.Web;
using Common.Results;
using ServerMessenger.Helpers;

namespace DbWorker
{
    public class DbSystemWorker : IDbSystemWorker
    {
        public DbSystemWorker()
        {
            using (var context = new MessengerContext())
            {
                context.Database.CreateIfNotExists();
            }

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MessengerContext, Migrations.Configuration>());
        }

        public async Task<string> AddEmployee(Employee employee)
        {
            var password = Helper.CreateRandomPassword(8);
            using (var context = new MessengerContext())
            {
                var dbEmployee = new Db.Employee
                {
                    Surname = employee.Surname,
                    Name = employee.Name,
                    Patronymic = employee.Patronymic,
                    Gender = employee.Gender,
                    DateOfBirth = employee.DateOfBirth,
                    Position = employee.Position,
                };

                context.Employee.Add(dbEmployee);

                var dbUser = new Db.User()
                {
                    Email = employee.Email,
                    Login = employee.Login,
                    Password = password.HashPassword()
                };

                context.User.Add(dbUser);
                await context.SaveChangesAsync();
                return password;
            }
        }

        public async Task RemoveEmployee(Employee employee)
        {
            using (var context = new MessengerContext())
            {
                var dbEmployee = await context.Employee.SingleOrDefaultAsync(x =>
                    x.Surname.Equals(employee.Surname)
                    && x.Name.Equals(employee.Name)
                    && x.Patronymic.Equals(employee.Patronymic));
                
                if (dbEmployee == null)
                {
                    return;
                }

                var userGuid = dbEmployee.Guid;
                var dbUser = await context.User.SingleAsync(x => x.Guid == userGuid);

                context.Employee.Remove(dbEmployee);
                context.User.Remove(dbUser);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Employee> GetEmployee(string login, string password)
        {
            var hashPassword = password.HashPassword();
            using (var context = new MessengerContext())
            {
                var dbUser = await context.User.SingleOrDefaultAsync(x => (x.Login == login || x.Email == login) && x.Password == hashPassword);
                if (dbUser == null)
                {
                    return null;
                }

                var dbEmployee = dbUser.Employee;
                var employee = new Employee();
                employee.Guid = dbUser.Guid;
                employee.Login = dbUser.Login;
                employee.Email = dbUser.Email;
                employee.Surname = dbEmployee.Surname;
                employee.Name = dbEmployee.Name;
                employee.Patronymic = dbEmployee.Patronymic;
                employee.Position = dbEmployee.Position;
                employee.Gender = dbEmployee.Gender;
                employee.DateOfBirth = dbEmployee.DateOfBirth;
                employee.Photo = dbEmployee.Photo != null ? Convert.FromBase64String(dbEmployee.Photo) : null;
                return employee;
            }
        }

        public async Task<List<Employee>> GetEmployees()
        {
            using (var context = new MessengerContext())
            {
                var employees = new List<Employee>();
                var dbEmployees = await context.Employee.ToListAsync();
                foreach (var x in dbEmployees)
                {
                    var employee = new Employee();

                    employee.Guid = x.Guid;
                    employee.Surname = x.Surname;
                    employee.Name = x.Name;
                    employee.Patronymic = x.Patronymic;
                    employee.DateOfBirth = x.DateOfBirth;
                    employee.Position = x.Position;
                    employee.Gender = x.Gender;

                    var photo = x.Photo == null ? null : Convert.FromBase64String(x.Photo);
                    employee.Photo = photo;

                    var user = x.User;
                    employee.Login = user.Login;
                    employee.Email = user.Email;

                    employees.Add(employee);
                }
                
                return employees;
            }
        }

        public async Task<Dictionary<Guid, List<Party>>> GetChats(Guid userGuid)
        {
            using (var context = new MessengerContext())
            {
                var dbParties = await context.Party.Where(x => x.UserGuid == userGuid)
                    .GroupBy(x => x.ChatGuid)
                    .ToDictionaryAsync(x =>
                        x.Key, p =>
                        p.Where(x => x.UserGuid != userGuid)
                            .Select(x => new Party { UserGuid = x.UserGuid, ChatGuid = x.ChatGuid}).ToList());
                return dbParties;
            }
        }

        public async Task<List<Party>> GetUserChats(Guid userGuid)
        {
            using (var context = new MessengerContext())
            {
                var dbUserChatGuids = await context.Party.Where(x => x.UserGuid == userGuid).Select(x => x.ChatGuid).ToListAsync();
                var dbParties = await context.Party.Where(x => dbUserChatGuids.Contains(x.ChatGuid))
                    .Select(x => new Party {ChatGuid = x.ChatGuid, UserGuid = x.UserGuid}).ToListAsync();
                return dbParties;
            }
        }

        public async Task<Guid> SaveMessage(Guid selfGuid, Guid chatOrUserGuid, string message, DateTime date)
        {
            using (var context = new MessengerContext())
            {
                Guid chatGuid;
                var messageGuid = Guid.NewGuid();
                var dbParties = context.Party.Where(x => x.ChatGuid == chatOrUserGuid);
                if (dbParties.Any())
                {
                    var dbMessage = new Db.Message
                    {
                        Guid = messageGuid,
                        UserGuid = selfGuid,
                        Content = message,
                        ChatGuid = chatOrUserGuid,
                        DateCreate = date
                    };

                    context.Message.Add(dbMessage);

                    var partyUserGuids = dbParties.Select(x => x.UserGuid);
                    List<Db.MessageStatus> messageStatuses = new List<Db.MessageStatus>();
                    foreach (var partyUserGuid in partyUserGuids)
                    {
                        var messageStatus = new Db.MessageStatus
                        {
                            MessageGuid = messageGuid,
                            IsRead = false,
                            UserGuid = partyUserGuid
                        };

                        messageStatuses.Add(messageStatus);
                    }

                    context.MessageStatus.AddRange(messageStatuses);
                    chatGuid = chatOrUserGuid;
                }
                else
                {
                    Guid dbChatGuid = Guid.NewGuid();
                    Db.Chat dbChat = new Db.Chat
                    {
                        Guid = dbChatGuid
                    };

                    context.Chat.Add(dbChat);

                    Db.Party party1 = new Db.Party
                    {
                        ChatGuid = dbChatGuid,
                        UserGuid = selfGuid,
                    };

                    Db.Party party2 = new Db.Party
                    {
                        ChatGuid = dbChatGuid,
                        UserGuid = chatOrUserGuid,
                    };

                    context.Party.Add(party1);
                    context.Party.Add(party2);

                    var dbMessage = new Db.Message
                    {
                        Guid = messageGuid,
                        UserGuid = selfGuid,
                        Content = message,
                        ChatGuid = dbChatGuid,
                        DateCreate = date
                    };

                    context.Message.Add(dbMessage);

                    var messageStatus1 = new Db.MessageStatus
                    {
                        MessageGuid = messageGuid,
                        IsRead = false,
                        UserGuid = selfGuid
                    };

                    var messageStatus2 = new Db.MessageStatus
                    {
                        MessageGuid = messageGuid,
                        IsRead = false,
                        UserGuid = chatOrUserGuid
                    };

                    context.MessageStatus.Add(messageStatus1);
                    context.MessageStatus.Add(messageStatus2);

                    chatGuid = dbChatGuid;
                }

                await context.SaveChangesAsync();

                return chatGuid;
            }
        }
        
        public async Task UpdatePhoto(Guid userGuid, string photo)
        {
            using (var context = new MessengerContext())
            {
                var user = context.Employee.Single(x => x.Guid == userGuid);
                user.Photo = photo;
                await context.SaveChangesAsync();
            }
        }

        public async Task<GetChatResult> GetChat(Guid chatGuid)
        {
            using (var context = new MessengerContext())
            {
                var dbMessages = await context.Message.Where(x => x.ChatGuid == chatGuid).ToListAsync();
                var history = new GetChatResult();
                history.Messages = new List<Message>();
                foreach (var dbMessage in dbMessages)
                {
                    var message = new Message();
                    message.UserGuid = dbMessage.UserGuid;
                    message.ChatGuid = dbMessage.ChatGuid;
                    message.Content = dbMessage.Content;
                    message.DateCreate = dbMessage.DateCreate;
                    message.Guid = dbMessage.Guid;

                    message.Employee = new Employee();
                    var dbUser = dbMessage.User;
                    message.Employee.Login = dbUser.Login;
                    message.Employee.Email = dbUser.Email;
                    message.Employee.Guid = dbUser.Guid;

                    var dbEmployee = dbUser.Employee;
                    message.Employee.Surname = dbEmployee.Surname;
                    message.Employee.Name = dbEmployee.Name;
                    message.Employee.Patronymic = dbEmployee.Patronymic;
                    message.Employee.Position = dbEmployee.Position;

                    message.MessageStatus = new MessageStatus();
                    foreach (var dbMessageMessageStatus in dbMessage.MessageStatuses)
                    {
                        message.MessageStatus.UserGuid = dbMessageMessageStatus.UserGuid;
                        message.MessageStatus.MessageGuid = dbMessageMessageStatus.MessageGuid;
                        message.MessageStatus.IsRead = dbMessageMessageStatus.IsRead;
                    }

                    history.Messages.Add(message);
                }

                return history;
            }

        }
    }
}
