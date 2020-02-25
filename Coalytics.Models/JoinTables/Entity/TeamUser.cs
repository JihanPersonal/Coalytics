using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coalytics.Models.Auth.Entity
{
    /// <summary>
    /// User Team Mapping Entity. 
    /// Represent the Many to Many relationship between Teams and Users
    /// </summary>
    public class TeamUser
    {
        /// <summary>
        /// Foreign Key for User Entity
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Foreign Key for Team Entity
        /// </summary>
        public string TeamId { get; set; }

    }
}
