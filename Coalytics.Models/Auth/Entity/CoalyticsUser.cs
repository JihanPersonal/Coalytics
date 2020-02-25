using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coalytics.Models.Auth.Entity;
using Microsoft.AspNetCore.Identity;

namespace Coalytics.Models.Auth
{
    /// <summary>
    /// Registered app user
    /// </summary>
    public class CoalyticsUser : IdentityUser
    {
        /// <summary>
        /// Constructor to Initialzie TeamUsers
        /// </summary>
        public CoalyticsUser()
        {
            UserTeams = new List<TeamUser>();
        }
        // TODO extend with relevant props
        /// <summary>
        /// Teams for the User
        /// </summary>
        public List<TeamUser> UserTeams { get; set; }
    }
}
