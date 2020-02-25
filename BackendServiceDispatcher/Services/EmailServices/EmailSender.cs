using BackendServiceDispatcher.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BackendServiceDispatcher.Services
{
    /// <summary>
    /// Email Service to Send Email or Appointment using SendGrid
    /// </summary>
    public class EmailSender : IEmailSender
    {
        private SendGridClient _client;
        private readonly IHostingEnvironment _hosting;
        private readonly IConfiguration _config;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hosting"></param>
        /// <param name="config"></param>
        public EmailSender(IHostingEnvironment hosting, IConfiguration config)
        {
            //Please Reference README.md to get the API Key 
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_KEY");           
            _client = new SendGridClient(apiKey);
            _hosting = hosting;
            _config = config;
        }
        /// <summary>
        /// Send Email by passing Email, Subject and message
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            EmailModel emailmodel = new EmailModel();
            emailmodel.ReciverEmail = email;
            emailmodel.ReceiverName = email;
            emailmodel.Subject = subject;
            emailmodel.TextContent = message;
            emailmodel.Type = EmailType.NoTemplates;
            await SendEmailAsync(emailmodel);
        }

        public async Task SendConfirmationEmailAsync(string email, string link)
        {
            EmailModel emailmodel = new EmailModel();
            emailmodel.ReciverEmail = email;
            emailmodel.ReceiverName = email;
            emailmodel.Subject = "Please Confirm Your Emails Address";
            emailmodel.Type = EmailType.EmailConfirmation;
            SetDefualtforEmailModel(emailmodel, link);
            await SendEmailAsync(emailmodel);
        }

        public async Task SendResetPasswordEmailAsync(string email, string link)
        {
            EmailModel emailmodel = new EmailModel();
            emailmodel.ReciverEmail = email;
            emailmodel.ReceiverName = email;
            emailmodel.Subject = "Please Reset Your Password";
            emailmodel.Type = EmailType.RestPassword;
            SetDefualtforEmailModel(emailmodel, link);
            await SendEmailAsync(emailmodel);
        }

        /// <summary>
        /// Send Email by passing EmailModel
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<HttpStatusCode> SendEmailAsync(EmailModel email)
        {
            var from = new EmailAddress(email.SenderEmail, email.SenderName);
            var subject = email.Subject;
            var to = new EmailAddress(email.ReciverEmail, email.ReceiverName);
            var plainTextContent = email.UsingHtmlContent ? String.Empty : email.TextContent;
            var htmlContent = email.HTMLContent;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await _client.SendEmailAsync(msg);
            return response.StatusCode;
        }

        /// <summary>
        /// Send Appointment by passing AppointmentModel 
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns>Return Status code as: Accepted, Unauthorized, BadRequest</returns>
        public async Task<HttpStatusCode> SendAppointmentAsync(AppointmentModel appointment)
        {
            if (!appointment.ValidAppoitment)
            {
                throw new InvalidDataException("Invaid appointment. Appointment StartTime must be earlier than EndTime");
            }

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(appointment.OrgnizerEmail, appointment.OrgnizerName),
                Subject = appointment.Subject,
                PlainTextContent = appointment.UsingHtmlContent ? String.Empty : appointment.TextContent,
                HtmlContent = appointment.HTMLContent,
            };
            var recipients = new List<EmailAddress>();
            appointment.Atteendees.ForEach(a => recipients.Add(new EmailAddress( a.Email, a.Name)));

            msg.AddTos(recipients);

            string CalendarContent = MeetingRequestString(appointment.OrgnizerName, appointment.Atteendees, appointment.Subject, appointment.TextContent, appointment.Location, appointment.StartTime,appointment.EndTime,appointment.IsCanlcel);
            byte[] calendarBytes = Encoding.UTF8.GetBytes(CalendarContent.ToString());
            Attachment calendarAttachment = new Attachment();
            calendarAttachment.Filename = "invite.ics";
            //the Base64 encoded content of the attachment.
            calendarAttachment.Content = Convert.ToBase64String(calendarBytes);
            calendarAttachment.Type = "text/calendar";
            msg.Attachments = new List<Attachment>() { calendarAttachment };

            var response = await _client.SendEmailAsync(msg);
            return response.StatusCode;
        }

        private void SetDefualtforEmailModel(EmailModel email, string link="")
        {
            email.SenderName = _config["EmailSenderConfig:SenderName"];
            email.SenderEmail = _config["EmailSenderConfig:SenderEmail"];
            HTMLContextTypebyEmailType(email);
            email.HTMLContent = email.HTMLContent.Replace("{{name}}", email.ReceiverName);
            if (email.Type==EmailType.EmailConfirmation)
            {
                email.HTMLContent = email.HTMLContent.Replace("{{LifeSpan}}", _config["TokenLifeSpan:EmailConfirmation"]);
            }
            else if (email.Type==EmailType.RestPassword)
            {
                email.HTMLContent = email.HTMLContent.Replace("{{LifeSpan}}", _config["TokenLifeSpan:ResetPassword"]);
            }
            if (link!=String.Empty)
            {
                email.HTMLContent=email.HTMLContent.Replace("{{action_url}}", link);
            }
        }
        private void HTMLContextTypebyEmailType(EmailModel email)
        {
            string resource = String.Empty;
            string path = Path.Combine(_hosting.ContentRootPath, @"Services\EmailServices\EmailTemplates");
            switch (email.Type)
            {
                case EmailType.Welcome:
                    resource = "Welcome.html";
                    break;
                case EmailType.EmailConfirmation:
                    resource = "EmailConfirmation.html";
                    break;
                case EmailType.RestPassword:
                    resource = "ResetPassword.html";
                    break;
                case EmailType.NoTemplates:
                    resource = String.Empty;
                    break;
            }
            if (resource != String.Empty)
            {
                string html = System.IO.File.ReadAllText(Path.Combine(path, resource));
                email.HTMLContent = html;

            }
        }

        private static string MeetingRequestString(string from, List<Atteendee> atteendees, string subject, string desc, string location, DateTime startTime, DateTime endTime, bool isCancel, int? eventID = null)
        {
            StringBuilder str = new StringBuilder();
            List<string> toUsers = atteendees.Select(i => i.Name).ToList();
            str.AppendLine("BEGIN:VCALENDAR");
            str.AppendLine("PRODID:-//Microsoft Corporation//Outlook 12.0 MIMEDIR//EN");
            str.AppendLine("VERSION:2.0");
            str.AppendLine(string.Format("METHOD:{0}", (isCancel ? "CANCEL" : "REQUEST")));
            str.AppendLine("BEGIN:VEVENT");

            str.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", startTime.ToUniversalTime()));
            str.AppendLine(string.Format("DTSTAMP:{0:yyyyMMddTHHmmss}", DateTime.Now));
            str.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmssZ}", endTime.ToUniversalTime()));
            str.AppendLine(string.Format("LOCATION: {0}", location));
            str.AppendLine(string.Format("UID:{0}", (eventID.HasValue ? "blablabla" + eventID : Guid.NewGuid().ToString())));
            str.AppendLine(string.Format("DESCRIPTION:{0}", desc.Replace("\n", "<br>")));
            str.AppendLine(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", desc.Replace("\n", "<br>")));
            str.AppendLine(string.Format("SUMMARY:{0}", subject));

            str.AppendLine(string.Format("ORGANIZER;CN=\"{0}\":MAILTO:{1}", from, from));
            str.AppendLine(string.Format("ATTENDEE;CN=\"{0}\";RSVP=TRUE:mailto:{1}", string.Join(",", toUsers), string.Join(",", toUsers)));

            str.AppendLine("BEGIN:VALARM");
            str.AppendLine("TRIGGER:-PT15M");
            str.AppendLine("ACTION:DISPLAY");
            str.AppendLine("DESCRIPTION:Reminder");
            str.AppendLine("END:VALARM");
            str.AppendLine("END:VEVENT");
            str.AppendLine("END:VCALENDAR");

            return str.ToString();
        }
    }
}
