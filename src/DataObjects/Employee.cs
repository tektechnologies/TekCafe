using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
   public class Employee
    {
        public int EmployeeID { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public List<string> Roles { get; private set; }

        public Employee(int employeeID, string firstName, string lastName, List<string> roles)
        {
            this.EmployeeID = employeeID;
            this.FirstName = firstName;
            this.LastName = LastName;
            this.Roles = roles;
        }
    }
}
