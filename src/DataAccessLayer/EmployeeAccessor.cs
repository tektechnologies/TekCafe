using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class EmployeeAccessor
    {

        public static int VerifyEmployeeNameAndPassword(string employeename, string passwordHash)
        {
            int result = 0;
            //result will be one if authenticated
            var conn = DBConnection.GetDBConnection();

            // next, we need a command object
            var cmd = new SqlCommand("sp_authorize_employee", conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add any needed parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            // set the values for the parameters
            cmd.Parameters["@Email"].Value = employeename;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            // now the command is set up, time to execute it
            // but database code is unsafe code, so try-catch it

            try
            {
                //open connection
                conn.Open();
                // execute the command
                result = (int)cmd.ExecuteScalar();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static Employee GetEmployeeByEmail(string email)
        {
            Employee employee = null;

            var conn = DBConnection.GetDBConnection();

            string cmdText1 = @"sp_get_employee_info_by_email";
            string cmdText2 = @"sp_get_employee_roles";

            var cmd1 = new SqlCommand(cmdText1, conn);
            var cmd2 = new SqlCommand(cmdText2, conn);

            // command types
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd2.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd1.Parameters.Add("@Email", SqlDbType.NVarChar, 255);
            cmd2.Parameters.Add("@Email", SqlDbType.NVarChar, 255);

            // parameter values
            cmd1.Parameters["@Email"].Value = email;
            cmd2.Parameters["@Email"].Value = email;



            try
            {
                int employeeID = 0;
                string firstName = null;
                string lastName = null;

                List<string> roles = new List<string>();

                conn.Open();
                SqlDataReader theReader = cmd1.ExecuteReader();

                if (theReader.HasRows)
                {
                    theReader.Read();

                    employeeID = theReader.GetInt32(0);
                    firstName = theReader.GetString(1);
                    lastName = theReader.GetString(2);
                }
                else
                {
                    throw new ApplicationException("Employee not found.");
                }
                //close this reader so that other readers can run
                theReader.Close();


                //here is the second reader for the second command execute reader.//////////

                //run the second reader
                SqlDataReader theSecondReader = cmd2.ExecuteReader();
                if (theSecondReader.HasRows)
                {
                    while (theSecondReader.Read())
                    {
                        string role = theSecondReader.GetString(0);
                        //add roles to employee role
                        roles.Add(role);
                    }
                }
                theSecondReader.Close();
                //group employee attributes into the employee objects
                //Build the second employee object based on email 
                employee = new Employee(employeeID, firstName, lastName, roles);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return employee;
        }


        public static int UpdatePassword(string employeename, string newPassword, string oldPassword)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            
            string cmdText = "sp_update_newpassword_hash";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

          
            // parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@OldPasswordHash", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.NVarChar, 100);

            // values
            cmd.Parameters["@Email"].Value = employeename;
            cmd.Parameters["@OldPasswordHash"].Value = oldPassword;
            cmd.Parameters["@NewPasswordHash"].Value = newPassword;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }


        public static List<string> GetRoles(string employeename)
        {
            List<string> roles = new List<string>();

            // need a connection
            var conn = DBConnection.GetDBConnection();
            // command text
            string cmdText3 = @"sp_get_employee_roles";

            // command objects
            var cmd3 = new SqlCommand(cmdText3, conn);

            // command types
            cmd3.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd3.Parameters.Add("@Email", SqlDbType.NVarChar, 255);

            // parameter values
            cmd3.Parameters["@Email"].Value = employeename;

            // do it!
            try
            {
                // open the connection
                conn.Open();

                // process cmd2
                SqlDataReader theThirdReader = cmd3.ExecuteReader();

                if (theThirdReader.HasRows)
                {
                    while (theThirdReader.Read())
                    {
                        string role = theThirdReader.GetString(0);
                        // add to roles
                        roles.Add(role);
                    }
                }
                theThirdReader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return roles;
        }




























    }

}
