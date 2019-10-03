using Employees;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeTests
{
    public class CsvRepositoryTest
    {
        [Fact]
        public async Task FindEmployees_ReturnsListOfAllmployees_IfInvoked()
        {
            CsvRepository csvRepository = new CsvRepository();
            var result = await csvRepository.FindEmployees("EmployeeData.csv");
            Assert.Equal(5, result.Count);
        }
    }
}
