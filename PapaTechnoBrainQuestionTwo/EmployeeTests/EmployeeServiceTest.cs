using Employees;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EmployeeTests
{
   public class EmployeeServiceTest
    {
        [Fact]
        public void ValidateAllEmployees_ThrowsException_WhenThereExistsMoreThanOneCompanyCeo()
        {
            List<Employee> employees = new List<Employee>
            {
                Employee.AddNewEmployee("Employee1","",100),
                Employee.AddNewEmployee("Employee2",null,100)
               
            };
            EmployeeService service = new EmployeeService(employees);
            service.ValidateAllEmployees();
            Assert.False(service.IsAuthentic);
            Assert.Contains(service.ExceptionLogger, m => m.Message == "More than one CEO has been Found");
        }

        [Fact]
        public void VerifyIfAllManagersAreDisplayed_ThrowsException_WhenSomeManagersAreNotDisplayed()
        {
            List<Employee> employees = new List<Employee>
            {
                Employee.AddNewEmployee("Employee1","Employee2",100),
                Employee.AddNewEmployee("Employee2","Employee3",100)
            };
            EmployeeService employeeService = new EmployeeService(employees);
            employeeService.ValidateAllEmployees();
            Assert.False(employeeService.IsAuthentic);
            Assert.Contains(employeeService.ExceptionLogger, m => m.Message == "Not All Managers Have Been Displayed");
        }

        [Fact]
        public void ValidateEmployeeWithMoreThanOneManager_ThrowsException_WhenEmployeeHasMoreThanOneManager()
        {
            List<Employee> employees = new List<Employee>
            {
                Employee.AddNewEmployee("Employee1","",100),
                Employee.AddNewEmployee("Employee2","Employee1",100),
                Employee.AddNewEmployee("Employee3","Employee1",100),
                Employee.AddNewEmployee("Employee3","Employee2",100)
            };
            EmployeeService employeeService = new EmployeeService(employees);
            employeeService.ValidateAllEmployees();
            Assert.False(employeeService.IsAuthentic);
            Assert.Contains(employeeService.ExceptionLogger, m => m.Message == "Employee EmployeeThree has more than one manager");
        }

        [Fact]
        public void VerifyCircularReferencing_ThrowsException_WhenEmployeesHaveCircularReference()
        {
            List<Employee> employees = new List<Employee>
            {
                Employee.AddNewEmployee("Employee0","",100),
                Employee.AddNewEmployee("Employee2","Employee1",100),
                Employee.AddNewEmployee("Employee1","Employee2",100)
            };
            EmployeeService employeeService = new EmployeeService(employees);
            employeeService.ValidateAllEmployees();
            Assert.False(employeeService.IsAuthentic);
            Assert.Contains(employeeService.ExceptionLogger, m => m.Message == "Circular Reference Occured");
        }
        [Theory]
        [InlineData("")]
        public void FindManagerBudget_ThrowsArgumentNullException_WhenIdIsNotAuthentic(string managerId)
        {
            EmployeeService employeeService = new EmployeeService(new List<Employee>());
            Assert.Throws<ArgumentNullException>(nameof(managerId), () => employeeService.FindManagerBudget(managerId));
        }
        [Theory]
        [InlineData("Employee2", 1800)]
        [InlineData("Employee3", 500)]
        [InlineData("Employee1", 3800)]
        public void GetManagersBudget_ReturnsBudget(string managerId, long expected)
        {
            List<Employee> employees = new List<Employee>
            {
                Employee.AddNewEmployee("Employee1","",1000),
                Employee.AddNewEmployee("Employee2","Employee1",800),
                Employee.AddNewEmployee("Employee3","Employee1",500),
                Employee.AddNewEmployee("Employee4","Employee2",500),
                Employee.AddNewEmployee("Employee6","Employee2",500),
                Employee.AddNewEmployee("Employee5","Employee1",500)
            };
            EmployeeService employeeService = new EmployeeService(employees);
            var result = employeeService.FindManagerBudget(managerId);
            Assert.Equal(expected, result);
        }
    }
}
