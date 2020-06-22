using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Request;
using Common.Results;

namespace DbWorker
{
    public interface IDbSystemWorker
    {
        Task<string> AddEmployee(Employee employee);
        Task RemoveEmployee(Employee employee);
        Task<Employee> GetEmployee(string login, string password);
        Task<List<Employee>> GetEmployees();
        Task<List<Chat>> GetUserChats(Guid userGuid);
        Task SaveMessage(Guid selfGuid, Guid chatGuid, string message, DateTime date);
        Task UpdatePhoto(Guid userGuid, string photo);
        Task<GetChatResult> GetChat(Guid chatGuid);
        Task CreateGroupChat(string chatName, Guid creatorGuid, List<Guid> userGuids);
        Task CreateChat(Guid userGuid, Guid creatorGuid);
        Task<List<Guid>> GetUserFromChat(Guid chatGuid);
        Task<ResultBody> ChangePassword(Guid selfGuid, string oldPassword, string newPassword);
    }
}
