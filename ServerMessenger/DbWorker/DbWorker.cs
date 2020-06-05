using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Db = DbWorker.DbElements;
using System.Web;
using ServerMessenger.Helpers;

namespace DbWorker
{
    public class DbSystemWorker : IDbSystemWorker
    {
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
                    Password = password.GetPasswordHash()
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
                var dbEmployee = context.Employee.SingleOrDefault(x =>
                    x.Surname.Equals(employee.Surname)
                    && x.Name.Equals(employee.Name)
                    && x.Patronymic.Equals(employee.Patronymic));
                
                if (dbEmployee == null)
                {
                    return;
                }

                var userGuid = dbEmployee.Guid;
                var dbUser = context.User.Single(x => x.Guid == userGuid);

                context.Employee.Remove(dbEmployee);
                context.User.Remove(dbUser);
                await context.SaveChangesAsync();
            }
        }
    }
}
