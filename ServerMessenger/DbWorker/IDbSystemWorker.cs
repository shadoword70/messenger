using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DbWorker
{
    public interface IDbSystemWorker
    {
        Task<string> AddEmployee(Employee employee);
        Task RemoveEmployee(Employee employee);
        Task<Employee> GetEmployee(string login, string password);
        Task<List<Employee>> GetEmployees();
        Task<Dictionary<Guid, List<Party>>> GetChats(Guid userGuid);
        Task<List<Party>> GetUserChats(Guid userGuid);
        Task<Guid> SaveMessage(Guid selfGuid, Guid chatOrUserGuid, string message, DateTime date);
    }
}
