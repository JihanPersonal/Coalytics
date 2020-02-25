using Coalytics.Models.Auth.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Coalytics.DataAccess.Data
{
    /// <summary>
    /// Repository for Data Access
    /// </summary>
    public class CoalyticsRepository : ICoalyticsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        /// <summary>
        /// Constructor to initialize dbContext
        /// </summary>
        /// <param name="dbContext"></param>
        public CoalyticsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region CoalyticsTeam
        /// <summary>
        /// Get All Teams
        /// </summary>
        /// <returns></returns>
        public List<CoalyticsTeam> GetAllTeams()
        {
            return _dbContext.CoalyticsTeams
                    .Include(t => t.TeamUsers)
                    .Include(t => t.TeamProjects).ToList();
        }
        /// <summary>
        /// Get A Team by Name 
        /// </summary>
        /// <param name="teamName"></param>
        /// <returns></returns>
        public CoalyticsTeam GetTeambyTeamName(string teamName)
        {
            return _dbContext.CoalyticsTeams
                    .Include(t => t.TeamUsers)
                    .Include(t => t.TeamProjects)
                    .Where(t => t.TeamName == teamName).FirstOrDefault();

        }
        /// <summary>
        /// Add a New Team
        /// </summary>
        /// <param name="teamName"></param>
        public void AddTeam(string teamName)
        {
            CoalyticsTeam team = GetTeambyTeamName(teamName);
            if (team == null)
            {
                _dbContext.CoalyticsTeams.Add(new CoalyticsTeam()
                {
                    TeamName = teamName,
                    TeamId = Guid.NewGuid().ToString()
                });
                Save();
            }
        }
        /// <summary>
        /// Delete Existing Team 
        /// </summary>
        /// <param name="teamName"></param>
        public void DeleteTeam(string teamName)
        {
            CoalyticsTeam team = GetTeambyTeamName(teamName);
            if (team != null)
            {
                _dbContext.CoalyticsTeams.Remove(team);
                Save();
            }
        }

        #endregion CoalyticsTeam

        #region CoalyticsProject
        /// <summary>
        /// Get all Projects
        /// </summary>
        /// <returns></returns>
        public List<CoalyticsProject> GetAllProjects()
        {
            return _dbContext.CoalyticsProjects
                             .Include(p => p.ProjectTeams)
                             .Include(p => p.ProjectFormComponents).ToList();
        }
        /// <summary>
        /// Get a Project by Name
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public CoalyticsProject GetProjectbyProjectName(string projectName)
        {
            return _dbContext.CoalyticsProjects
                    .Include(t => t.ProjectTeams)
                    .Include(t=>t.ProjectFormComponents)
                    .Where(p => p.ProjectName == projectName).FirstOrDefault();

        }
        /// <summary>
        /// Add a new Project
        /// </summary>
        /// <param name="projectName"></param>
        public void AddProject(string projectName)
        {
            CoalyticsProject project = GetProjectbyProjectName(projectName);
            if (project == null)
            {
                _dbContext.CoalyticsProjects.Add(new CoalyticsProject()
                {
                    ProjectName = projectName,
                    ProjectId = Guid.NewGuid().ToString()
                });

                Save();
            }
        }
        /// <summary>
        /// Delete Existing Project
        /// </summary>
        /// <param name="projectName"></param>
        public void DeleteProject(string projectName)
        {
            CoalyticsProject project = GetProjectbyProjectName(projectName);

            if (project != null)
            {
                _dbContext.CoalyticsProjects.Remove(project);
                Save();
            }
        }
        #endregion CoalyticsProject

        #region FormComponent
        /// <summary>
        /// Return All FormComponents
        /// </summary>
        /// <returns></returns>
        public List<FormComponent> GetAllFormComponents()
        {
            return _dbContext.FormComponents
                             .Include(c => c.FormComponentProjects)
                             .ToList();
        }
        /// <summary>
        /// Get a FormComponent by FormComponentName
        /// </summary>
        /// <param name="componentName"></param>
        /// <returns></returns>
        public FormComponent GetFormComponentbyName(string componentName)
        {
            return _dbContext.FormComponents
                .Where(t => t.FormComponentName == componentName)
                .FirstOrDefault();
        }
        /// <summary>
        /// Add new FormComponent
        /// </summary>
        /// <param name="componentName"></param>
        /// <param name="typeName"></param>
        public void AddFormComponent(string componentName, string typeName)
        {
            FormComponent component = GetFormComponentbyName(componentName);
            FormComponentType type = GetFormComponentTypebyTypeName(typeName);
            if (component == null && type != null)
            {
                _dbContext.FormComponents.Add(new FormComponent
                {
                    FormComponentId = Guid.NewGuid().ToString(),
                    FormComponentName = componentName,
                    FormComponentTypeId = type.FormComponentTypeId
                });
                Save();
            }
        }
        /// <summary>
        /// Delete Existing FormComponent
        /// </summary>
        /// <param name="componentName"></param>
        public void DeleteFormComponent(string componentName)
        {
            FormComponent component = GetFormComponentbyName(componentName);
            if (component != null)
            {
                _dbContext.FormComponents.Remove(component);
            }
            Save();
        }

        #endregion FormComponent

        #region FormComponentType
        /// <summary>
        /// Get all FormComponentTypes
        /// </summary>
        /// <returns></returns>
        public List<FormComponentType> GetAllFormComponentTypes()
        {
            return _dbContext.FormComponentTypes
                             .Include(c => c.FormComponents)
                             .ToList();
        }
        /// <summary>
        /// Get a FormComponentType by TypeName 
        /// </summary>
        /// <param name="TypeName"></param>
        /// <returns></returns>
        public FormComponentType GetFormComponentTypebyTypeName(string typeName)
        {
            return _dbContext.FormComponentTypes
                .Include(t => t.FormComponents)
                .Where(t => t.TypeName == typeName)
                .FirstOrDefault();
        }
        /// <summary>
        /// Add new FormComponentType
        /// </summary>
        /// <param name="typeName"></param>
        public void AddFormComponentType(string typeName)
        {
            FormComponentType type = GetFormComponentTypebyTypeName(typeName);
            if (type == null)
            {
                _dbContext.FormComponentTypes.Add(new FormComponentType()
                {
                    TypeName = typeName,
                    FormComponentTypeId = Guid.NewGuid().ToString()
                });
                Save();
            }
        }
        /// <summary>
        /// Delete Existing FormComponentType
        /// </summary>
        /// <param name="typeName"></param>
        public void DeleteFormComponentType(string typeName)
        {
            FormComponentType type = GetFormComponentTypebyTypeName(typeName);
            if (type != null)
            {
                _dbContext.FormComponentTypes.Remove(type);
                Save();
            }
        }
        #endregion FormComponentType

        /// <summary>
        /// Save Data change to DB
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
           return _dbContext.SaveChanges() > 0;
        }

    }
}
