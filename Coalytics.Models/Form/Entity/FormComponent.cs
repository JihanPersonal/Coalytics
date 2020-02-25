using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coalytics.Models.Auth.Entity
{
    /// <summary>
    /// FormComponent Entity. Liked with Projects
    /// </summary>
    public class FormComponent
    {
        /// <summary>
        /// Constructor to Initialzie FormComponentProjects
        /// </summary>
        public FormComponent()
        {
            FormComponentProjects = new List<ProjectFormComponent>();
        }
        /// <summary>
        /// FormComponent Id. Primary Key
        /// </summary>
        public string FormComponentId { get; set; }
        /// <summary>
        /// FormComponent Name
        /// </summary>
        public string FormComponentName { get; set; }
        /// <summary>
        /// FormComponent Type. Foreign Key FormComponentType 
        /// </summary>
        public string FormComponentTypeId { get; set; }
        /// <summary>
        /// Projects contains the FromComponent
        /// </summary>
        public List<ProjectFormComponent> FormComponentProjects { get; set; }
    }
}
