using BackendServiceDispatcher.Models;
using Coalytics.DataAccess.Data;
using Coalytics.Models.Auth.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BackendServiceDispatcher.Controllers
{
    /// <summary>
    ///  Controller to handle FormComponentTypes
    /// </summary>
    [Route("api/[Controller]")]
    public class FormComponentTypeController: Controller
    {
        private readonly ICoalyticsRepository _repository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public FormComponentTypeController(ICoalyticsRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// Get All FormComponentTypes 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllFormComponentTypes()
        {
            var formtypes = _repository.GetAllFormComponentTypes();
            if (formtypes.Count()==0)
            {
                return Ok("There is no FormComponentType");
            }
            return Ok(_repository.GetAllFormComponentTypes());
        }

        /// <summary>
        /// Get FormComponent by Type
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> GetFormComponentsbyType([FromBody]FormComponentTypeModel model)
        {
            if (ModelState.IsValid)
            {
                FormComponentType type = _repository.GetFormComponentTypebyTypeName(model.FormComponentTypeName);

                if (type == null)
                {
                    return BadRequest("Invalid FormComponent Type");
                }

                return Ok(type.FormComponents);
            }
            return BadRequest();
        }

        /// <summary>
        /// Add new FormComponentType
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> AddFormComponentType([FromBody]FormComponentTypeModel model)
        {
            if (ModelState.IsValid)
            {
                FormComponentType type = _repository.GetFormComponentTypebyTypeName(model.FormComponentTypeName);

                if (type != null)
                {
                    return BadRequest("ComponentType already Exists");
                }

                _repository.AddFormComponentType(model.FormComponentTypeName);

                return Ok("ComponentType has been created");
            }
            return BadRequest();
        }
        /// <summary>
        /// Delete Existing FormComponentType
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteFormComponentType([FromBody]FormComponentTypeModel model)
        {
            if (ModelState.IsValid)
            {
                FormComponentType type = _repository.GetFormComponentTypebyTypeName(model.FormComponentTypeName);

                if (type == null)
                {
                    return BadRequest("ComponentType doesn't Exists");
                }

                if (type.FormComponents.Count() > 0)
                {
                    return BadRequest("ComponentType Cannot be deleted. There are FormComponents using this Type");
                }

                _repository.DeleteFormComponentType(model.FormComponentTypeName);

                return Ok("ComponentType has been Removed");
            }
            return BadRequest();
        }
    }
}
