using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
   public class ProjectManager
    {
        public bool EditProject(Project oldProject, Project newProject)
        {
            bool result = false;

            try
            {
                result = (1 == ProjectAccessor.UpdateProject(oldProject, newProject));
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public bool AddProject(Project newProject)
        {
            bool result = false;

            try
            {
                result = (1 == ProjectAccessor.InsertProject(newProject));
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }


        public List<Project> GetProjectsByPhase(string phase = "All")
        {
            List<Project> projects = null;
            if (phase != "")
            {
                try
                {
                   projects = ProjectAccessor.SelectProjectByPhase(phase);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return projects;
        }


        public List<string> GetProjectTypes()
        {
            List<string> projectTypes = null;

            try
            {
                projectTypes = ProjectAccessor.SelectAllProjectTypes();
            }
            catch (Exception)
            {
                throw;
            }

            return projectTypes;
        }


        public List<string> GetAllProjectPhase()
        {
            List<string> projectPhase = null;

            try
            {
                projectPhase = ProjectAccessor.SelectAllProjectPhase();
            }
            catch (Exception)
            {
                throw;
            }

            return projectPhase;
        }


        

    }
}
