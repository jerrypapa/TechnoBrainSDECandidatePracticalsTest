using Employees;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeTests
{
   public class EmployeesTest
    {
        private readonly Employees_ employee;
        private string CsvFilePath = @"EmployeeData.csv";
        private readonly Mock<ICsvRepository> csvRepoMock = new Mock<ICsvRepository>();
        public EmployeesTest()
        {
            employee = new Employees_(CsvFilePath, csvRepoMock.Object);
        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Employees_ThrowsArgumentNullException_WhenCSVPathIsNotAuthentic(string path)
        {
            Assert.Throws<ArgumentNullException>(() => new Employees_(path, csvRepoMock.Object));
        }
        [Fact]
        public async Task GetEmployeeRecords_ReturnsNull_WhenListIsNull()
        {
            csvRepoMock.Setup(k => k.FindEmployees(CsvFilePath)).ReturnsAsync((List<Employee>)null);
            var result = await employee.FindEmployeeRecords();
            Assert.Null(result);
        }

        [Fact]
        public async Task Employees_ThrowsAggregateException_WhenEmployeesAreNotAuthentic()
        {
            List<Employee> employees = new List<Employee>
            {
                Employee.AddNewEmployee("Employee0","",100),
                Employee.AddNewEmployee("Employee2","Employee1",100),
                Employee.AddNewEmployee("Employee1","Employee2",100),
                Employee.AddNewEmployee("Employee1","",1100)
            };
            csvRepoMock.Setup(k => k.FindEmployees(CsvFilePath)).ReturnsAsync(employees);
            await Assert.ThrowsAsync<AggregateException>(async () => await employee.FindEmployeeRecords());
        }
        [Fact]
        public async Task GetEmployeeRecords_ReturnsEmployeesDetails()
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
            csvRepoMock.Setup(k => k.FindEmployees(CsvFilePath)).ReturnsAsync(employees);
            var result = await employee.FindEmployeeRecords();
            Assert.Equal(6, result.Count);
        }
        [Fact]
        public async Task FindManagerBudgetReturnsZeroWhenEmployeeListIsNull()
        {
            csvRepoMock.Setup(k => k.FindEmployees(CsvFilePath)).ReturnsAsync((List<Employee>)null);
            var result = await employee.FindManagerBudget("Employee1");
            Assert.Equal(0, result);
        }
        [Theory]
        [InlineData("Employee2", 1800)]
        [InlineData("Employee3", 500)]
        [InlineData("Employee1", 3800)]
        public async Task FindManagerBudgetReturnsManagersBudget(string employeeId, long budget)
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
            csvRepoMock.Setup(k => k.FindEmployees(CsvFilePath)).ReturnsAsync(employees);
            var result = await employee.FindManagerBudget(employeeId);
            Assert.Equal(budget, result);
        }
    }
}
