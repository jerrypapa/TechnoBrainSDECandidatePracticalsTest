using Employees;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EmployeeTests
{
    public class EmployeeTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void CreateThrowsArgumentNullException_WhenIdIsNotAuthentic(string id)
        {
            Assert.Throws<ArgumentNullException>(nameof(id), () => Employee.AddNewEmployee(id, null, default(decimal)));
        }

        [Fact]
        public void CreateThrowsArgumentOutOfRangeException_WhenSalaryIsNotAuthentic()
        {
            decimal salary = -1;
            Assert.Throws<ArgumentOutOfRangeException>(nameof(salary), () => Employee.AddNewEmployee("Empl001", null, salary));
        }
        [Fact]
        public void CreateCreatesNewEmployee()
        {
            var employee = Employee.AddNewEmployee("Employee1", "", 100);
            Assert.Equal("Employee1", employee.Id);
            Assert.Equal(100, employee.Salary);
        }
    }
}
