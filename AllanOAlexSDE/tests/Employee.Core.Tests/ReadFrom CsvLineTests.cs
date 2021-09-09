using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Employee.Core.Tests
{
	public class ReadFrom_CsvLineTests
	{
		[Fact]
		public void ReadEmployeeFromCSVLine_Returns_NewEmployee()
		{
			string line = "Employee1,Employee0,100";
			var employee = ReadFromCsvLine.ReadEmployeeFromCsvLine(line);
			Assert.Equal("Employee0", employee.ManagerId);
		}
		[Fact]
		public void ReadEmployeeFromCSVLine_ReturnsNull_WhenInputIsInvalid()
		{
			string line = "Employee1,Employee0100";
			var employee = ReadFromCsvLine.ReadEmployeeFromCsvLine(line);
			Assert.Null(employee);
		}
	}
}
