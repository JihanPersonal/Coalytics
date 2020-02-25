using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coalytics.Models.Auth.Entity
{
    /// <summary>
    /// Team Entity. Maybe can be replaced by Role?
    /// </summary>
    public class CoalyticsTeam
    {
        /// <summary>
        /// Constructor to Initialize TeamUsers
        /// </summary>
        public CoalyticsTeam()
        {
            TeamUsers = new List<TeamUser>();
            TeamProjects = new List<ProjectTeam>();
        }
        /// <summary>
        /// Team Id. Primary Key 
        /// </summary>
        public string TeamId { get; set; }
        /// <summary>
        /// Team Name
        /// </summary>
        public string TeamName { get; set; }
        /// <summary>
        /// Users in the Team
        /// </summary>
        public List<TeamUser> TeamUsers { get; set; }
        /// <summary>
        /// Projects for the Team
        /// </summary>
        public List<ProjectTeam> TeamProjects { get; set; }

    }
}
