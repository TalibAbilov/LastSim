using FluentValidation;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace KiderApp.ViewModels.Account
{
    public class LoginVm
    {
        public string? EmailOrUserName { get; set; }
        public string? Password { get; set; }
        public bool Reminder {  get; set; }
    }
    public class LoginVmValidator : AbstractValidator<LoginVm>
    {
        public LoginVmValidator()
        {
            RuleFor(x => x.EmailOrUserName).NotEmpty().WithMessage("EmailOrUserName doldur!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password doldur!");

        }
    }
}
