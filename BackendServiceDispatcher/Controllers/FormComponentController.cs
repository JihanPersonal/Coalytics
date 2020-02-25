using BackendServiceDispatcher.Models;
using Coalytics.DataAccess.Data;
using Coalytics.Models.Auth.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BackendServiceDispatcher.Controllers
{
    /// <summary>
    /// Controller to handle  FormComponent
    /// </summary>
    [Route("api/[Controller]")]
    public class FormComponentController: Controller
    {
        private readonly ICoalyticsRepository _repository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public FormComponentController(ICoalyticsRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get All GetAllFormComponents 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllFormComponents()
        {
            var forms = _repository.GetAllFormComponents();
            if (forms.Count()==0)
            {
                return Ok("There is no FormComponent");
            }
            return Ok(_repository.GetAllFormComponents());
        }


        /// <summary>
        /// Get Projects contain the FormComponent
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> GetFormComponentProjects([FromBody]FormComponentModel model)
        {
            if (ModelState.IsValid)
            {
                FormComponent component = _repository.GetFormComponentbyName(model.FormComponentName);

                if (component == null)
                {
                    return BadRequest("FormComponent doesn't Exist");
                }

                return Ok(component.FormComponentProjects);
            }
            return BadRequest();
        }
        
        /// <summary>
        /// Add New FormComponent
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> AddFormComponent([FromBody]FormComponentModel model)
        {
            if (ModelState.IsValid)
            {
                FormComponentType type = _repository.GetFormComponentTypebyTypeName(model.FormComponentTypeName);

                if (type==null)
                {
                    return BadRequest("Invalid FormComponent Type");
                }

                FormComponent component = _repository.GetFormComponentbyName(model.FormComponentName);

                if (component!=null)
                {
                    return BadRequest("FormComponent with the same name already Exists");
                }

                _repository.AddFormComponent(model.FormComponentName, model.FormComponentTypeName);

                return Ok("FormComponent has been Created");
            }
            return BadRequest();
        }
        /// <summary>
        /// Delete Existing FormComponent
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteFormComponent([FromBody]FormComponentModel model)
        {
            if (ModelState.IsValid)
            {

                FormComponent component = _repository.GetFormComponentbyName(model.FormComponentName);

                if (component == null)
                {
                    return BadRequest("FormComponent doesn't Exist");
                }
                _repository.DeleteFormComponent(model.FormComponentName);

                return Ok("FormComponent has been Deleted");
            }
            return BadRequest();
        }

    }
}
