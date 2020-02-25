using BackendServiceDispatcher.Models;
using Coalytics.DataAccess.Data;
using Coalytics.Models.Auth;
using Coalytics.Models.Auth.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BackendServiceDispatcher.Controllers
{
    /// <summary>
    /// Controller to handle Coalytics Teams 
    /// </summary>
    [Route("api/[Controller]")]
    public class TeamController : Controller
    {
        private readonly ICoalyticsRepository _repository;
        private readonly UserManager<CoalyticsUser> _userManager;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="userManager"></param>
        public TeamController(ICoalyticsRepository repository,
            UserManager<CoalyticsUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        /// <summary>
        /// Get All Teams
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllTeams()
        {
            var teams = _repository.GetAllTeams();

            if (teams.Count()==0)
            {
                return Ok("There is no Team");
            }

            return Ok(teams);
        }

        /// <summary>
        /// Create New Coalytics Team
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> AddTeam([FromBody]CoalyticsTeamModel model)
        {
            if (ModelState.IsValid)
            {
                if (_repository.GetTeambyTeamName(model.TeamName) != null)
                {
                    return BadRequest("Team with the same Name already Exists");
                }

                _repository.AddTeam(model.TeamName);
                _repository.Save();

                return Ok("Team has been Added");
            }
            return BadRequest();
        }
        
        /// <summary>
        /// Delete Existing Coalytics Team
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteTeam([FromBody]CoalyticsTeamModel model)
        {
            if (ModelState.IsValid)
            {
                CoalyticsTeam team = _repository.GetTeambyTeamName(model.TeamName);
                if (team==null)
                {
                    return BadRequest("Cannot Find the Team");
                }
                else
                {
                    if (team.TeamProjects.Count() > 0)
                    {
                        return BadRequest("Cannot Delete Team. There are projects assigned for this Team");
                    }
                    else
                    {
                        _repository.DeleteTeam(model.TeamName);
                    }
                }
                return Ok("Team has been Deleted");
            }
            return BadRequest();
        }
        /// <summary>
        /// Get all users in the Team
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> GetTeamUsers([FromBody]CoalyticsTeamModel model)
        {
            if (ModelState.IsValid)
            {
                CoalyticsTeam team = _repository.GetTeambyTeamName(model.TeamName);
                if (team == null)
                {
                    return BadRequest("Cannot Find the Team");
                }
                else
                {
                    return Ok(team.TeamUsers);
                }
            }
            return BadRequest();
        }
        /// <summary>
        /// Get all Projects assigned for the Team
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> GetTeamProjects([FromBody]CoalyticsTeamModel model)
        {
            if (ModelState.IsValid)
            {
                CoalyticsTeam team = _repository.GetTeambyTeamName(model.TeamName);
                if (team == null)
                {
                    return BadRequest("Cannot Find the Team");
                }
                else
                {
                    return Ok(team.TeamProjects);
                }
            }
            return BadRequest();
        }
        /// <summary>
        /// Add User to Team
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> AddUserToTeam([FromBody]TeamUserModel model)
        {
            if (ModelState.IsValid)
            {
                CoalyticsTeam team = _repository.GetTeambyTeamName(model.TeamName);
                if (team == null)
                {
                    return BadRequest("Cannot Find the Team");
                }
                else
                {
                    CoalyticsUser user =await _userManager.FindByNameAsync(model.UserName);
                    if (user ==null)
                    {
                        return BadRequest("Cannot Find the User");
                    }
                    else if (team.TeamUsers.Where(u=>u.UserId==user.Id).Count()>0)
                    {
                        return BadRequest("User already in Team");
                    }
                    else
                    {
                        team.TeamUsers.Add(new TeamUser()
                        {
                            TeamId = team.TeamId,
                            UserId = user.Id
                        });
                        _repository.Save();
                    }
                }
                return Ok("User has been added into Team");
            }
            return BadRequest();
        }
        /// <summary>
        /// Delete User from Team
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteUserFromTeam([FromBody]TeamUserModel model)
        {
            if (ModelState.IsValid)
            {
                CoalyticsUser user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    return BadRequest("Cannot Find the User");
                }

                CoalyticsTeam team = _repository.GetTeambyTeamName(model.TeamName);

                if (team == null)
                {
                    return BadRequest("Cannot Find the Team");
                }
                else
                {
                    TeamUser teamuser = team.TeamUsers.Where(t => t.UserId == user.Id).FirstOrDefault();
                    if (teamuser == null)
                    {
                        return BadRequest("User not in the Team");
                    }
                    else
                    {
                        team.TeamUsers.Remove(teamuser);
                        _repository.Save();
                    }
                }
                return Ok("User has been removed from Team");
            }
            return BadRequest();
        }

    }
}

