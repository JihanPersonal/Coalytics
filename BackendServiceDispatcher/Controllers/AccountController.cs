using BackendServiceDispatcher.Extensions;
using BackendServiceDispatcher.Models.AccountDataModels;
using BackendServiceDispatcher.Services;
using Coalytics.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BackendServiceDispatcher.Controllers
{
    /// <summary>
    /// Account Endpoint
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        // TODO refactor to repository
        private readonly UserManager<CoalyticsUser> _userManager;
        private readonly SignInManager<CoalyticsUser> _signInManager;

        private readonly IEmailSender _emailSender;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="emailSender"></param>
        public AccountController(
            UserManager<CoalyticsUser> userManager,
            SignInManager<CoalyticsUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        /// <summary>
        /// A Test GET API
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]        
        public IActionResult Get()
        {
            return new OkObjectResult("api/Account/");
        }

        /// <summary>
        /// User Registration API
        /// </summary>
        /// <param name="registrationDataModel"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody]RegistrationDataModel registrationDataModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CoalyticsUser applicationUser = new CoalyticsUser() { UserName = registrationDataModel.Email, Email = registrationDataModel.Email };
            var result = await _userManager.CreateAsync(applicationUser, registrationDataModel.Password);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
                var callbackUrl = Url.EmailConfirmationLink(applicationUser.Id, code, Request.Scheme);
                await _emailSender.SendEmailConfirmationAsync(registrationDataModel.Email, callbackUrl);
                await _signInManager.SignInAsync(applicationUser, isPersistent: false);
                return new OkResult();
            }
            return new BadRequestObjectResult(result.Errors);            
        }

        /// <summary>
        /// ForgotPassword API 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordDataModel model)
        {           
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetUrl = Url.Action("ResetPassword", "Account",
                        new { token = token, email = user.Email }, Request.Scheme);
                    await _emailSender.SendResetPasswordEmailAsync(model.Email, resetUrl);
                }
                else
                {
                    await _emailSender.SendEmailAsync(model.Email, "ResetPassword", "You don't have an Account with this Email Address");
                }

                return new OkResult();
            }
            return BadRequest();
        }

        /// <summary>
        /// ResetPassword API
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordDataModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View();
                    }

                    if (await _userManager.IsLockedOutAsync(user))
                    {
                        await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
                    }
                    return new OkResult();
                }
            }
            return new BadRequestObjectResult("Invalid Request");
        }

        /// <summary>
        /// Email Confirmation API
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return new BadRequestObjectResult("userId and code are required");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return new OkResult();
        }
    }
}
