using System;
using System.Collections.Generic;
using System.Text;

namespace Budweg.Model
{
    public class Employee
    {
        public int EmployeeID { get; set; }

        public Employee()
        {
        }

        public Employee(int employeeID) // Constructor
        {
            EmployeeID = employeeID;
        }

        public override string ToString()
        {
            return $"{EmployeeID}";
        }
    }
}
