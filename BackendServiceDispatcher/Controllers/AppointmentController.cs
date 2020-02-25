using BackendServiceDispatcher.Models;
using BackendServiceDispatcher.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BackendServiceDispatcher.Controllers
{
    /// <summary>
    /// Controller to handle Appointment 
    /// </summary>
    [Route("api/[Controller]")]
    public class AppointmentController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly AppointmentModel _appointment;
        private readonly IConfiguration _config;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="emailSender"></param>
        /// <param name="config"></param>
        public AppointmentController(IEmailSender emailSender, IConfiguration config)
        {
            _emailSender = emailSender;
            _config = config;
            _appointment = new AppointmentModel
            {

                OrgnizerName = _config["AppointMentConfig:OrganizerName"],
                OrgnizerEmail = _config["AppointMentConfig:OrganizerEmail"],
                Atteendees = new List<Atteendee>
                {
                    new Atteendee
                    {
                        Name = _config["AppointMentConfig:Attendee:Name"],
                        Email = _config["AppointMentConfig:Attendee:Email"]
                    }
                },
                StartTime = DateTime.Now.AddMinutes(30),
                EndTime = DateTime.Now.AddMinutes(60),
                IsCanlcel = false,
                TextContent = _config["AppointMentConfig:Content"],
                Subject = _config["AppointMentConfig:Subject"]
            };
        }

        /// <summary>
        /// Get API. using hardcoded _appointment to test the Appointment Service 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                HttpStatusCode status =await _emailSender.SendAppointmentAsync(_appointment);
                if (status == HttpStatusCode.Accepted)
                {
                    return Ok("Appointment Sent Sucessfully");
                }
                else if (status==HttpStatusCode.Unauthorized)
                {
                    return BadRequest("Unauthorized to Send Appointment");
                }
                else
                {
                    return BadRequest("Failed to Send Appointment");
                }
            }
            catch
            {
                return BadRequest("Failed to Send Appointment");
            }
        }
        /// <summary>
        /// Post Api. Get API. 
        /// </summary>
        /// <param name="appointment">
        /// Body of Appointment Model
        /// </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AppointmentModel appointment)
        {
            try
            {
                HttpStatusCode status = await _emailSender.SendAppointmentAsync(appointment);
                if (status == HttpStatusCode.Accepted)
                {
                    return Ok("Appointment Sent Sucessfully");
                }
                else if (status == HttpStatusCode.Unauthorized)
                {
                    return BadRequest("Unauthorized to Send Appointment");
                }
                else
                {
                    return BadRequest("Failed to Send Appointment");
                }
            }
            catch
            {
                return BadRequest("Failed to Send Appointment");
            }
        }
    }
}

