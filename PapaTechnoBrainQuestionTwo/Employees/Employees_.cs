using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
   public class Employees_
    {
        private string CsvFilePath;
        private readonly ICsvRepository CsvRepository;
        public Employees_(string cSvPath, ICsvRepository csvRepository)
        {
            CsvFilePath = string.IsNullOrWhiteSpace(cSvPath) ? throw new ArgumentNullException(nameof(cSvPath)) : cSvPath;
            CsvRepository = csvRepository;
        }

        private Employees_() { }
        public async Task<long> FindManagerBudget(string managerId)
        {
            var employeesDetails = await CsvRepository.FindEmployees(CsvFilePath);

            if (employeesDetails != null)
            {
                EmployeeService services = new EmployeeService(employeesDetails);
                return services.FindManagerBudget(managerId);
            }
            return 0;
        }
        public async Task<List<Employee>> FindEmployeeRecords()
        {
            var employeesDetails = await CsvRepository.FindEmployees(CsvFilePath);
            if (employeesDetails != null)
            {
                EmployeeService services = new EmployeeService(employeesDetails);
                services.ValidateAllEmployees();
                if (services.IsAuthentic)
                {
                    return employeesDetails;
                }
                throw new AggregateException(services.ExceptionLogger);
            }
            return null;
        }
    }
}
