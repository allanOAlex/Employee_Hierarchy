using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core
{
	public static class ReadFromCsvLine
	{
		static public Employee ReadEmployeeFromCsvLine(this string csvLine)
		{
			string[] parts = csvLine.Split(',');

			if (parts.Length == 3)
			{
				string Id = parts[0];
				string ManagerId = parts[1];
				string Salary = parts[2];

				long.TryParse(Salary, out long salary);

				return Employee.Create(Id, ManagerId, salary);
			}

			return null;
		}
	}
}
