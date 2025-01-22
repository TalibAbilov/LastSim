using FluentValidation;

namespace KiderApp.Areas.Manage.ViewModels.Agent
{
    public class CreateAgentVm
    {
        public string? FullName { get; set; }
        public string? ImgUrl {  get; set; }
        public IFormFile? file { get; set; }
        public int? DesignationId { get; set; }
    }
    public class CreateAgentVmValidator : AbstractValidator<CreateAgentVm>
    {
        public CreateAgentVmValidator()
        {
            RuleFor(x=>x.FullName).NotEmpty().WithMessage("FullName doldur!").MaximumLength(20).WithMessage("Max len 20!").MinimumLength(3).WithMessage("Min len 3!");
            RuleFor(x => x.file).NotEmpty().WithMessage("File sec!");
            RuleFor(x => x.DesignationId).NotEmpty().WithMessage("Designation sec!");
        }
    }
}
