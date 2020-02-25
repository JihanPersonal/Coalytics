using System.Collections.Generic;

namespace Coalytics.Models.Auth.Entity
{
    /// <summary>
    /// Project Entity
    /// </summary>
    public class CoalyticsProject
    {
        /// <summary>
        /// Constructor to Initialize ProjectTeams
        /// </summary>
        public CoalyticsProject()
        {
            ProjectTeams = new List<ProjectTeam>();
            ProjectFormComponents = new List<ProjectFormComponent>();
        }
        /// <summary>
        /// Project Id. Primary Key
        /// </summary>
        public string ProjectId { get; set; }
        /// <summary>
        /// Project Name
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// Teams for the Project
        /// </summary>
        public List<ProjectTeam> ProjectTeams { get; set; }
        /// <summary>
        /// FormComponents for Project
        /// </summary>
        public List<ProjectFormComponent> ProjectFormComponents { get; set; }
    }
}
