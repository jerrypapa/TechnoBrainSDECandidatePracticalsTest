using Employees;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EmployeeTests
{
   public class FetchEmployeeFromCsvTest
    {
        [Fact]
        public void FetchEmployeeFromCsvReturnsNewEmployee()
        {
            string Csvline = "Employee1,Employee0,100";
            var employee = FetchEmployeeFromCsv.GetDetailsFromCsvLine(Csvline);
            Assert.Equal("Employee0", employee.ManagerId);
        }
        [Fact]
        public void FetchEmployeeFromCsvReturnsNullWhenInputIsNotAuthentic()
        {
            string Csvline = "Employee1,Employee03500";
            var employee = FetchEmployeeFromCsv.GetDetailsFromCsvLine(Csvline);
            Assert.Null(employee);
        }
    }
}
