using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coalytics.Models.Auth.Entity
{
    /// <summary>
    /// FormComponentType Entity
    /// </summary>
    public class FormComponentType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FormComponentType()
        {
            FormComponents = new List<FormComponent>();
        }
        /// <summary>
        /// FormComponentType Id. Primary Key. 
        /// </summary>
        public string FormComponentTypeId { get; set; }
        /// <summary>
        /// Referenced by FormComponent
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// FormComponents for the Type
        /// </summary>
        public List<FormComponent> FormComponents { get; set; }
    }
}
