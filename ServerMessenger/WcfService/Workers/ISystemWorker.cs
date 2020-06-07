using System.Threading.Tasks;
using Common;

namespace WcfService.Workers
{
    public interface ISystemWorker
    {
        Task AddEmployee(Employee employee);
    }
}
