using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coalytics.Models.Auth.Entity
{
    /// <summary>
    /// ProjectFormComponentMapping Entity.
    /// Represent Many to Many Relationship between Projects and FormComponents 
    /// </summary>
    public class ProjectFormComponent
    {
        /// <summary>
        /// Foreign Key for Project Entity 
        /// </summary>
        public string ProjectId { set; get; }
        /// <summary>
        /// oreign Key for FormComponent Entity 
        /// </summary>
        public string FormComponentId { set; get; }
    }
}
