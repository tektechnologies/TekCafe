using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;



namespace LogicLayer
{
   public class EmployeeManager
    {
        //authentication method. 
        public Employee AuthenticateEmployee(string employeeName, string password)
        {
            Employee employee = null;
            bool isNewEmployee = (password == "password");
           
            //Step one is to hash password
            password = hashSHA256(password);


            //call methods from data access layer
            try
            {
                //does user exsist
                if(1 == EmployeeAccessor.VerifyEmployeeNameAndPassword(employeeName, password))
                {
                    //if found create an employee object
                    employee = EmployeeAccessor.GetEmployeeByEmail(employeeName);

                    if (isNewEmployee == true)
                    {
                        //first clear roles so that employees have enforced access to info
                        employee.Roles.Clear();
                        //then for new employees , added to new employee role. 
                        employee.Roles.Add("New Employee.");
                    }

                }
                else
                {
                    throw new ApplicationException("Login Credentials Invalid, try again.");
                }                
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Employee Not Found");
            }
            return employee;
        }

        //Method to Hash our password 

        private string hashSHA256(string source)
        {
            string result = "";

            // create a byte array - cryptography is byte-oriented
            //https:////en.wikipedia.org/wiki/Advanced_Encryption_Standard
            byte[] data;

        // create a .NET hash provider object
        //https:////docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.hashalgorithm?view=netframework-4.7.2
            using (SHA256 sha256hash = SHA256.Create())
            {
                // hash the input value
                data = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            }

            // now to build the string for the result
            var newS = new StringBuilder();
            // loop through the byte array creating letters
            for (int i = 0; i < data.Length; i++)
            {
                newS.Append(data[i].ToString("x2"));
            }
            result = newS.ToString();

            return result;
        }


        public bool UpdatePassword(string employeename, string newPassword, string oldPassword)
        {
            bool result = false;
            newPassword = hashSHA256(newPassword);
            oldPassword = hashSHA256(oldPassword);

            try
            {
                result = (1 == EmployeeAccessor.UpdatePassword(employeename, newPassword, oldPassword));
            }

            catch(Exception)
            {
                throw;
            }
            return result;
        }


        public void RefreshRoles(Employee employee, string employeename)
        {
            //role clearing complete - robot voice.
            employee.Roles.Clear();

            //get roles 
            var roles = EmployeeAccessor.GetRoles(employeename);

            //add roles as iterate through roles
            foreach (var role in roles)
            {
                employee.Roles.Add(role);
            }
            return;
        }
        



















    }
}
