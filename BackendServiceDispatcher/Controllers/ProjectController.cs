using BackendServiceDispatcher.Models;
using Coalytics.DataAccess.Data;
using Coalytics.Models.Auth.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendServiceDispatcher.Controllers
{
    /// <summary>
    /// Controler to handle Coalytics Project 
    /// </summary>
    [Route("api/[Controller]")]
    public class ProjectController : Controller
    {
        private readonly ICoalyticsRepository _repository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public ProjectController(ICoalyticsRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// Get All Projects 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllProjects()
        {
            var proejcts = _repository.GetAllProjects();
            if (proejcts.Count()==0)
            {
                return Ok("There is no Project");
            }
            return Ok(_repository.GetAllProjects());
        }

        /// <summary>
        /// Create New Project
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> AddProject([FromBody]CoalyticsProjectModel model)
        {
            if (ModelState.IsValid)
            {
                CoalyticsProject project = _repository.GetProjectbyProjectName(model.ProjectName);
                if (project!=null)
                {
                    return BadRequest("Project with the same Name already Exists");
                }

                _repository.AddProject(model.ProjectName);

                return Ok("Project has been Created");
            }
            return BadRequest();
        }
        /// <summary>
        /// Delete Existing Project 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteProject([FromBody]CoalyticsProjectModel model)
        {
            if (ModelState.IsValid)
            {
                CoalyticsProject project = _repository.GetProjectbyProjectName(model.ProjectName);
                if (project==null)
                {
                    return BadRequest("Cannot find the Project");
                }

                _repository.DeleteProject(model.ProjectName);

                return Ok("Project has been Deleted");
            }
            return BadRequest();
        }
        /// <summary>
        /// Add Team to Project
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> AddTeamToProject([FromBody]ProjectTeamModel model)
        {
            if (ModelState.IsValid)
            {
                CoalyticsProject project = _repository.GetProjectbyProjectName(model.ProjectName);
                if (project == null)
                {
                    return BadRequest("Cannot find the Project");
                }

                CoalyticsTeam team = _repository.GetTeambyTeamName(model.TeamName);
                if (team==null)
                {
                    return BadRequest("Cannot find the Team");
                }


                if (project.ProjectTeams.Where(t=>t.TeamId==team.TeamId).Count()>0)
                {
                    return BadRequest("Team has already been added for the Project");
                }

                project.ProjectTeams.Add(new ProjectTeam
                {
                    ProjectId=project.ProjectId,
                    TeamId=team.TeamId
                });
                _repository.Save();
                return Ok("Team has been added for Project");
            }
            return BadRequest();
        }
        /// <summary>
        /// Delete Team from Project
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteTeamFromProject([FromBody]ProjectTeamModel model)
        {
            if (ModelState.IsValid)
            {
                CoalyticsProject project = _repository.GetProjectbyProjectName(model.ProjectName);
                if (project == null)
                {
                    return BadRequest("Cannot find the Project");
                }

                CoalyticsTeam team =_repository.GetTeambyTeamName(model.TeamName);
                if (team == null)
                {
                    return BadRequest("Cannot find the Team");
                }

                ProjectTeam pt = project.ProjectTeams.Where(p => p.TeamId == team.TeamId).FirstOrDefault();

                if (pt ==null)
                {
                    return BadRequest("Team is not assigned to the Project");
                }

                project.ProjectTeams.Remove(pt);
                _repository.Save();
                return Ok("Team has been deleted from Project");
            }
            return BadRequest();
        }
        /// <summary>
        /// Add FormComponent To Project
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> AddFormComponentToProject([FromBody]ProjectFormComponentModel model)
        {
            if (ModelState.IsValid)
            {
                CoalyticsProject project = _repository.GetProjectbyProjectName(model.ProjectName);
                if (project == null)
                {
                    return BadRequest("Cannot find the Project");
                }

                FormComponent form = _repository.GetFormComponentbyName(model.FormComponentName);
                if (form == null)
                {
                    return BadRequest("Cannot find the FormComponent");
                }

                ProjectFormComponent pf = project.ProjectFormComponents.Where(p => p.FormComponentId == form.FormComponentId).FirstOrDefault();

                if (pf != null)
                {
                    return BadRequest("Project already contins FormComponent");
                }

                project.ProjectFormComponents.Add(new ProjectFormComponent()
                {
                    ProjectId = project.ProjectId,
                    FormComponentId = form.FormComponentId
                });

                _repository.Save();
                return Ok("FormComponent has been Added for Project");
            }
            return BadRequest();
        }
        /// <summary>
        /// Delete FormComponent From Project
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteFormComponentFromProject([FromBody]ProjectFormComponentModel model)
        {
            if (ModelState.IsValid)
            {
                CoalyticsProject project = _repository.GetProjectbyProjectName(model.ProjectName);
                if (project == null)
                {
                    return BadRequest("Cannot find the Project");
                }

                FormComponent form = _repository.GetFormComponentbyName(model.FormComponentName);
                if (form == null)
                {
                    return BadRequest("Cannot find the FormComponent");
                }

                ProjectFormComponent pf = project.ProjectFormComponents.Where(p => p.FormComponentId == form.FormComponentId).FirstOrDefault();

                if (pf == null)
                {
                    return BadRequest("Project doesn't contian FormComponent");
                }

                project.ProjectFormComponents.Remove(pf);
                _repository.Save();
                return Ok("FormComponent has been deleted from Project");
            }
            return BadRequest();
        }
        /// <summary>
        /// Get Teams for the Project
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> GetProjectTeams([FromBody]ProjectTeamModel model)
        {
            if (ModelState.IsValid)
            {
                CoalyticsProject project = _repository.GetProjectbyProjectName(model.ProjectName);
                if (project == null)
                {
                    return BadRequest("Cannot find the Project");
                }
                if (project.ProjectTeams.Count()==0)
                {
                    return Ok("Project has not been assigned to any team");
                }
                List<CoalyticsTeam> teams = _repository.GetAllTeams().
                    Where(t => project.ProjectTeams.Select(pt => pt.TeamId).Contains(t.TeamId)).
                    ToList();
                return Ok(teams);
            }
            return BadRequest();
        }
        /// <summary>
        /// Get FormComponents belongs to the Project
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> GetProjectFormComponents([FromBody]ProjectTeamModel model)
        {
            if (ModelState.IsValid)
            {
                CoalyticsProject project = _repository.GetProjectbyProjectName(model.ProjectName);
                if (project == null)
                {
                    return BadRequest("Cannot find the Project");
                }

                return Ok(project.ProjectFormComponents);
            }
            return BadRequest();
        }

    }
}
