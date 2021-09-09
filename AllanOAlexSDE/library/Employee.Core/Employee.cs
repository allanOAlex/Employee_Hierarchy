using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core
{
	public class Employee
	{
		public string Id { get; private set; }

		public string ManagerId { get; private set; }

		public long Salary { get; private set; }

		public Employee()
		{

		}

		public Employee(string empId, string managerId, long salary)
		{
			Id = empId.Trim();
			ManagerId = managerId;
			Salary = salary;
		}

		public static Employee Create(string empId, string managerId, long salary)
		{
			return new Employee(empId, managerId, salary);
		}

	}
}
