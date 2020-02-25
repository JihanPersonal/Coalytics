using System;

namespace BackendServiceDispatcher.Models
{
    /// <summary>
    /// Email Type
    /// </summary>
    public enum EmailType
    {
        /// <summary>
        /// Email to confirm Email Address
        /// </summary>
        EmailConfirmation,
        /// <summary>
        /// Email to welcome new User
        /// </summary>
        Welcome,
        /// <summary>
        /// Email to Reset Password
        /// </summary>
        RestPassword,
        /// <summary>
        /// Represent Plain Text Email
        /// </summary>
        NoTemplates
    }
    /// <summary>
    /// Email Model
    /// </summary>
    public class EmailModel
    {
        /// <summary>
        /// Email Type
        /// </summary>
        public EmailType Type { get; set; }
        /// <summary>
        /// ReceiverName 
        /// </summary>
        public string ReceiverName { get; set; }
        /// <summary>
        /// ReciverEmail
        /// </summary>
        public string ReciverEmail { get; set; }
        /// <summary>
        /// SenderName
        /// </summary>
        public string SenderName { set; get; }
        /// <summary>
        /// SenderEmail
        /// </summary>
        public string SenderEmail { set; get; }
        /// <summary>
        /// Subject
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// TextContent
        /// </summary>
        public string TextContent { get; set; }
        /// <summary>
        /// HTMLContent. Will be set by the Email Templates based on Email Type
        /// </summary>
        public string HTMLContent { get; set; }
        /// <summary>
        /// Indicator of whether use HTML Content
        /// </summary>
        public bool UsingHtmlContent
        {
            get
            {
                return HTMLContent == String.Empty;
            }
        }
    }
}
