using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
   public class CsvRepository:ICsvRepository
    {
        public async Task<List<Employee>> FindEmployees(string csvFilePath)
        {
            return await Task.Run(() =>
            {
                return File.ReadAllLines(csvFilePath).Skip(1).Where(o => o.Length > 1)
                    .Select(u => u.GetDetailsFromCsvLine()).ToList();
            });
        }
    }
}
