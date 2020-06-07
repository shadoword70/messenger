using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                employee.Login = dbUser.Login;
                employee.Email = dbUser.Email;
                employee.Surname = dbEmployee.Surname;
                employee.Name = dbEmployee.Name;
                employee.Patronymic = dbEmployee.Patronymic;
                employee.Position = dbEmployee.Position;
                employee.Gender = dbEmployee.Gender;
                employee.DateOfBirth = dbEmployee.DateOfBirth;
                return employee;
            }
        }
    }
}
