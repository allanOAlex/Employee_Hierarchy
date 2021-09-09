using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Employee.Core.Tests
{
	public class EmployeesTests
	{
		private readonly ICsvReader CsvReader;
		private readonly Employees Employees;
		private string CvsPath = @"D:/Data Center/Development/Assessments/TechnoBrain/SDE/AllanOAlexSDE/Files/Emp.csv";
		private readonly Mock<ICsvReader> CsvReaderMock = new Mock<ICsvReader>();


		public EmployeesTests()
		{
			Employees = new Employees(CvsPath, CsvReaderMock.Object);
		}


		[Theory]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData(null)]
		public void Employees_ThrowsArgumentNullException_WhenCSVPathIsInvalid(string path)
		{
			async Task csvPath() => new Employees(path, CsvReaderMock.Object);
			Assert.ThrowsAsync<ArgumentNullException>(csvPath);
		}


		[Fact]
		public async Task GetEmployeeRecords_ReturnsNull_WhenListIsNull()
		{
			CsvReaderMock.Setup(k => k.GetEmployees(CvsPath)).ReturnsAsync((List<Employee>)null);

			var result = await Employees.GetEmployeeRecords();
			Assert.Null(result);
		}


		[Fact]
		public async Task Employees_ThrowsAggregateException_WhenEmployeesAreInvald()
		{
			List<Employee> employees = new List<Employee>
			{
				Employee.Create("Employee0","",100),
				Employee.Create("Employee2","Employee1",100),
				Employee.Create("Employee1","Employee2",100),
				Employee.Create("Employee1","",1100)
			};

			CsvReaderMock.Setup(k => k.GetEmployees(CvsPath)).ReturnsAsync(employees);

			await Assert.ThrowsAsync<AggregateException>(async () => await Employees.GetEmployeeRecords());
		}


		[Fact]
		public async Task GetEmployeeRecords_ReturnsEmployees()
		{
			List<Employee> employees = new List<Employee>
			{
				Employee.Create("Employee1","",1000),
				Employee.Create("Employee2","Employee1",800),
				Employee.Create("Employee3","Employee1",500),
				Employee.Create("Employee4","Employee2",500),
				Employee.Create("Employee6","Employee2",500),
				Employee.Create("Employee5","Employee1",500)
			};

			CsvReaderMock.Setup(k => k.GetEmployees(CvsPath)).ReturnsAsync(employees);

			var result = await Employees.GetEmployeeRecords();
			Assert.Equal(6, result.Count);
		}


		[Fact]
		public async Task GetManagerBudget_ReturnsZero_WhenEmployeeListIsNull()
		{
			CsvReaderMock.Setup(k => k.GetEmployees(CvsPath)).ReturnsAsync((List<Employee>)null);

			var result = await Employees.GetManagerBudget("Employee1");
			Assert.Equal(0, result);
		}


		[Theory]
		[InlineData("Employee2", 1800)]
		[InlineData("Employee3", 500)]
		[InlineData("Employee1", 3800)]
		public async Task GetManagerBudget_ReturnsManagersBudget(string employeeId, long budget)
		{
			List<Employee> employees = new List<Employee>
			{
				Employee.Create("Employee1","",1000),
				Employee.Create("Employee2","Employee1",800),
				Employee.Create("Employee3","Employee1",500),
				Employee.Create("Employee4","Employee2",500),
				Employee.Create("Employee6","Employee2",500),
				Employee.Create("Employee5","Employee1",500)
			};

			CsvReaderMock.Setup(k => k.GetEmployees(CvsPath)).ReturnsAsync(employees);

			var result = await Employees.GetManagerBudget(employeeId);
			Assert.Equal(budget, result);
		}
	}
}
