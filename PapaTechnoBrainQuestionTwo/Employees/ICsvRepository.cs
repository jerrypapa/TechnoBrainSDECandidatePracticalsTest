using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
   public interface ICsvRepository
    {
        Task<List<Employee>> FindEmployees(string csvFilePath);
    }
}
