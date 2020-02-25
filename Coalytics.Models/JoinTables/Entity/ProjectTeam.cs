using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coalytics.Models.Auth.Entity
{
    /// <summary>
    /// ProjectTeamMapping Entity.
    /// Represent Many to Many Relationship between Projects and Teams 
    /// </summary>
    public class ProjectTeam
    {
        /// <summary>
        /// Foreign Key for Project Entity
        /// </summary>
        public string ProjectId { get; set; }
        /// <summary>
        /// Foreign Key for Team Entity
        /// </summary>
        public string TeamId { get; set; }
    }
}
