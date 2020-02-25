using Coalytics.Models.Auth.Entity;
using System.Collections.Generic;

namespace Coalytics.DataAccess.Data
{
    /// <summary>
    /// CoalyticsRepository Interface 
    /// </summary>
    public interface ICoalyticsRepository
    {
        #region CoalyticsTeam
        /// <summary>
        /// Get All Teams
        /// </summary>
        /// <returns></returns>
        List<CoalyticsTeam> GetAllTeams();
        /// <summary>
        /// Get A Team by Name 
        /// </summary>
        /// <param name="teamName"></param>
        /// <returns></returns>
        CoalyticsTeam GetTeambyTeamName(string teamName);
        /// <summary>
        /// Add a New Team
        /// </summary>
        /// <param name="teamName"></param>
        void AddTeam(string teamName);
        /// <summary>
        /// Delete Existing Team 
        /// </summary>
        /// <param name="teamName"></param>
        void DeleteTeam(string teamName);
        #endregion CoalyticsTeam

        #region CoalyticsProject
        /// <summary>
        /// Get all Projects
        /// </summary>
        /// <returns></returns>
        List<CoalyticsProject> GetAllProjects();
        /// <summary>
        /// Get a Project by Name
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        CoalyticsProject GetProjectbyProjectName(string projectName);
        /// <summary>
        /// Add a new Project
        /// </summary>
        /// <param name="projectName"></param>
        void AddProject(string projectName);
        /// <summary>
        /// Delete Existing Project
        /// </summary>
        /// <param name="projectName"></param>
        void DeleteProject(string projectName);
        #endregion CoalyticsProject

        #region FormComponentType
        /// <summary>
        /// Get all FormComponentTypes
        /// </summary>
        /// <returns></returns>
        List<FormComponentType> GetAllFormComponentTypes();
        /// <summary>
        /// Get a FormComponentType by TypeName 
        /// </summary>
        /// <param name="TypeName"></param>
        /// <returns></returns>
        FormComponentType GetFormComponentTypebyTypeName(string TypeName);
        /// <summary>
        /// Add new FormComponentType
        /// </summary>
        /// <param name="TypeName"></param>
        void AddFormComponentType(string TypeName);
        /// <summary>
        /// Delete Existing FormComponentType
        /// </summary>
        /// <param name="TypeName"></param>
        void DeleteFormComponentType(string TypeName);
        #endregion FormComponentType

        #region FormComponent
        /// <summary>
        /// Return All FormComponents
        /// </summary>
        /// <returns></returns>
        List<FormComponent> GetAllFormComponents();
        /// <summary>
        /// Get a FormComponent by FormComponentName
        /// </summary>
        /// <param name="componentName"></param>
        /// <returns></returns>
        FormComponent GetFormComponentbyName(string componentName);
        /// <summary>
        /// Add new FormComponent
        /// </summary>
        /// <param name="componentName"></param>
        /// <param name="typeName"></param>
        void AddFormComponent(string componentName, string typeName);
        /// <summary>
        /// Delete Existing FormComponent
        /// </summary>
        /// <param name="componentName"></param>
        void DeleteFormComponent(string componentName);
        #endregion FormComponent

        /// <summary>
        /// Save Data change to DB
        /// </summary>
        /// <returns></returns>
        bool Save();
    }
}