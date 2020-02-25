using BackendServiceDispatcher.Models.AccountDataModels.Validations;
using FluentValidation.Attributes;

namespace BackendServiceDispatcher.Models.AccountDataModels
{
    [Validator(typeof(RegistrationViewModelValidator))]
    public class RegistrationDataModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
