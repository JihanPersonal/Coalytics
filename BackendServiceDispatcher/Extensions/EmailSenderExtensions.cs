using BackendServiceDispatcher.Models;
using BackendServiceDispatcher.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace BackendServiceDispatcher.Extensions
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendConfirmationEmailAsync(email, link);
        }

        public static Task SendResetPasswordEmailAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendResetPasswordEmailAsync(email, link);
        }
    }
}
