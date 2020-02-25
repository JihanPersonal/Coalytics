using BackendServiceDispatcher.Models;
using System.Net;
using System.Threading.Tasks;

namespace BackendServiceDispatcher.Services
{
    public interface IEmailSender
    {
        /// <summary>
        /// Send Email by Email, Subject and Message
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendEmailAsync(string email, string subject, string message);

        Task SendConfirmationEmailAsync(string email, string link);

        Task SendResetPasswordEmailAsync(string email, string link);

        /// <summary>
        /// Send Appointment by AppointmentModel.
        /// </summary>
        /// <param name="appointmentModel"></param>
        /// <returns>Return Status code as: Accepted, Unauthorized, BadRequest</returns>
        Task<HttpStatusCode> SendAppointmentAsync(AppointmentModel appointmentModel);
    }
}
