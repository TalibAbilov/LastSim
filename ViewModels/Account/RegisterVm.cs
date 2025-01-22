using FluentValidation;

namespace KiderApp.ViewModels.Account
{
    public record RegisterVm
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }

    }
    public class RegisterVmValidator : AbstractValidator<RegisterVm>
    {
        public RegisterVmValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName doldur!").MaximumLength(20).WithMessage("Max len 20!").MinimumLength(3).WithMessage("Min len 3!");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName doldur!").MaximumLength(20).WithMessage("Max len 20!").MinimumLength(3).WithMessage("Min len 3!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email doldur!").EmailAddress().WithMessage("Emaili duzgun yaz!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password doldur!").MinimumLength(8).WithMessage("Min len 8!");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("ConfirmPassword doldur!").MinimumLength(8).WithMessage("Min len 8!").Matches(x=>x.Password).WithMessage("Passwordla uygunlasmir!");
        }
    }
}
