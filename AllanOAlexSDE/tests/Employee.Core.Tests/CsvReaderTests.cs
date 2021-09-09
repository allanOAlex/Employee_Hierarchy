using System;
using System.Threading.Tasks;
using Xunit;

namespace Employee.Core.Tests
{
	public class CsvReaderTests
	{
		[Fact]
		public async Task GetEmployees_ReturnsListOfAmployees_WhenCalled()
		{
			CsvReader csvReader = new CsvReader();
			var result = await csvReader.GetEmployees("D:/Data Center/Development/Assessments/TechnoBrain/SDE/AllanOAlexSDE/Files/EmpData.csv");
			Assert.Equal(6, result.Count);
		}
	}
}
