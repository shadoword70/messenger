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
    }
}
