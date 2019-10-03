using System;
using System.Collections.Generic;
using System.Text;

namespace Employees
{
   public static class FetchEmployeeFromCsv
    {
        static public Employee GetDetailsFromCsvLine(this string Line)
        {
            string[] CsvLinesections = Line.Split(',');
            if (CsvLinesections.Length == 3)
            {
                var Id = CsvLinesections[0];
                var EmployeeManagerId = CsvLinesections[1];
                var EmployeeSalary = CsvLinesections[2];
                decimal.TryParse(EmployeeSalary, out decimal salary);

                return Employee.AddNewEmployee(Id, EmployeeManagerId, salary);
            }
            return null;
        }
    }
}
