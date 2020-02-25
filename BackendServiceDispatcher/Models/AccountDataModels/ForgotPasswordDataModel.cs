using System.ComponentModel.DataAnnotations;

namespace BackendServiceDispatcher.Models.AccountDataModels
{
    public class ForgotPasswordDataModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
