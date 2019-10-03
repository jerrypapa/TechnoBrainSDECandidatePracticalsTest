using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
   public class EmployeeService
    {
       private List<Employee> employees_;
        public bool IsAuthentic { get; private set; } = true;
        public List<Exception> ExceptionLogger { get; private set; } = new List<Exception>();
        public EmployeeService(List<Employee> employees)
        {
            employees_ = employees ?? throw new ArgumentNullException(nameof(employees));
        }
        private EmployeeService() { }
        public void ValidateAllEmployees()
        {
            Parallel.Invoke(
                () => { VerifyNumberOfCompanyCeos(); },
                () => { ValidateEmployeeWithMoreThanOneManager(); },
                () => { VerifyIfAllManagersAreDisplayed(); },
                () => { VerifyCircularReferencing(); }
                );
        }

        private void VerifyNumberOfCompanyCeos()
        {
            if (employees_.Where(u => u.ManagerId == string.Empty || u.ManagerId == null).Count() > 1)
            {
                IsAuthentic = false;
                ExceptionLogger.Add(new Exception("More than one has been CEO Found"));
            }
        }
        private void VerifyIfAllManagersAreDisplayed()
        {
            foreach (var _ in employees_.Where(r => r.ManagerId != null && r.ManagerId != string.Empty)
                .Select(e => e.ManagerId).Where(manager => employees_.FirstOrDefault(e => e.Id == manager) == null).Select(manager => new { }))
            {
                IsAuthentic = false;
                ExceptionLogger.Add(new Exception("Not all managers have been listed"));
            }
        }
        private void ValidateEmployeeWithMoreThanOneManager()
        {
            foreach (var id in employees_.Select(e => e.Id).Distinct().Where(id => employees_.Where(i => i.Id == id)
            .Select(m => m.ManagerId).Distinct().Count() > 1).Select(id => id))
            {
                IsAuthentic = false;
                ExceptionLogger.Add(new Exception($"Employee {id} has more many managers"));
            }
        }
        private void VerifyCircularReferencing()
        {
            foreach (var _ in from employee in employees_.Where(e => e.ManagerId != string.Empty && e.ManagerId != null)
                              let manager = employees_.Where(e => e.ManagerId != string.Empty && e.ManagerId != null)
                        .FirstOrDefault(e => e.Id == employee.ManagerId)
                              where manager != null
                              where manager.ManagerId == employee.Id
                              select new { })
            {
                IsAuthentic = false;
                ExceptionLogger.Add(new Exception("Cyclic Reference Occurred"));
            }
        }
        public long FindManagerBudget(string managerId)
        {
            if (managerId == string.Empty) throw new ArgumentNullException(nameof(managerId));
            decimal TotalValueOfEmployeesSalary = 0;
            TotalValueOfEmployeesSalary += employees_.FirstOrDefault(b => b.Id == managerId).Salary;
            foreach (var item in employees_.Where(e => e.ManagerId == managerId))
            {
                // TotalValueOfEmployeesSalary += (EmployeeIsManager(manager.Id)) ? FindManagerBudget(manager.Id) : manager.Salary;
                if (EmployeeIsManager(item.Id))
                {
                    TotalValueOfEmployeesSalary += FindManagerBudget(item.Id);
                }
                else
                {
                    TotalValueOfEmployeesSalary += item.Salary;
                }
            }
            return Convert.ToInt32(TotalValueOfEmployeesSalary);
        }
        private bool EmployeeIsManager(string id) => employees_.Where(e => e.ManagerId == id).Count() > 0;
        
    }
}
