using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataObjects;

namespace DataAccessLayer
{
    public static class ProjectAccessor
    {
        public static List<Project> SelectProjectByPhase(string phase)
        {
            List<Project> projects = new List<Project>();
            var conn = DBConnection.GetDBConnection();
            string cmdText;
            SqlCommand cmd;

            if (phase == "All")
            {
                cmdText = "sp_get_tekcafeproject_by_id";
                cmd = new SqlCommand(cmdText, conn);
                cmd.CommandType = CommandType.StoredProcedure;
            }
            else
            {
                cmdText = "sp_get_tekcafeprojects_by_phase";
                cmd = new SqlCommand(cmdText, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PhaseID", phase);
            }
            try
            {
                conn.Open();
                var read = cmd.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        projects.Add(new Project()
                        {
                            ProjectID = read.GetString(0),
                            Name = read.GetString(1),
                            Description = read.GetString(2),
                            PurchaseDate = read.GetDateTime(3),
                            WorkStation = read.GetInt32(4),
                            ProjectTypeID = read.GetString(5),
                            PhaseID = read.GetString(6),
                            ClientID = read.GetString(7),
                            Active = read.GetBoolean(8)
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        
            return projects;
        }


        public static List<string> SelectAllProjectTypes()
        {
            List<string> projectTypes = new List<string>();
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_get_all_projecttypeid", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var read = cmd.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        projectTypes.Add(read.GetString(0));
                    }
                }
                read.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return projectTypes;
        }



        public static List<string> SelectAllProjectPhase()
        {
            List<string> phaseStatus = new List<string>();
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_get_tekcafeprojects_by_phase", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var read = cmd.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        phaseStatus.Add(read.GetString(0));
                    }
                }
                read.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return phaseStatus;
        }


        public static int UpdateProject(Project oldProject, Project newProject)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = "sp_update_project_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProjectID", oldProject.ProjectID);

            cmd.Parameters.AddWithValue("@Name", newProject.Name);
            cmd.Parameters.AddWithValue("@Description", newProject.Description);
            cmd.Parameters.AddWithValue("@PurchaseDate", newProject.PurchaseDate);
            cmd.Parameters.AddWithValue("@WorkStation", newProject.WorkStation);
            cmd.Parameters.AddWithValue("@ProjectTypeID", newProject.ProjectTypeID);
            cmd.Parameters.AddWithValue("@PhaseID", newProject.PhaseID);
            cmd.Parameters.AddWithValue("@ClientID", oldProject.ClientID);
            cmd.Parameters.AddWithValue("@Active", newProject.Active);

            cmd.Parameters.AddWithValue("@OldName", oldProject.Name);
            cmd.Parameters.AddWithValue("@OldDescription", oldProject.Description);
            cmd.Parameters.AddWithValue("@OldPurchaseDate", oldProject.PurchaseDate);
            cmd.Parameters.AddWithValue("@OldWorkStation", oldProject.WorkStation);
            cmd.Parameters.AddWithValue("@OldProjectTypeID", oldProject.ProjectTypeID);
            cmd.Parameters.AddWithValue("@OldPhaseID", oldProject.PhaseID);
            cmd.Parameters.AddWithValue("@OldClientID", oldProject.ClientID);
            cmd.Parameters.AddWithValue("@OldActive", oldProject.Active);

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



        public static int InsertProject(Project newProject)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = "sp_insert_project";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("ProjectID", newProject.ProjectID);
            cmd.Parameters.AddWithValue("@Name", newProject.Name);
            cmd.Parameters.AddWithValue("@Description", newProject.Description);
            cmd.Parameters.AddWithValue("@PurchaseDate", newProject.PurchaseDate);
            cmd.Parameters.AddWithValue("@WorkStation", newProject.WorkStation);
            cmd.Parameters.AddWithValue("@ProjectTypeID", newProject.ProjectTypeID);
            cmd.Parameters.AddWithValue("@PhaseID", newProject.PhaseID);
            cmd.Parameters.AddWithValue("@ClientID	", newProject.ClientID);

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

    }
}
