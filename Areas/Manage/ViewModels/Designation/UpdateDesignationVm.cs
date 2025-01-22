using FluentValidation;

namespace KiderApp.Areas.Manage.ViewModels.Designation
{
    public class UpdateDesignationVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class UpdateDesignationVmValidator : AbstractValidator<UpdateDesignationVm>
    {
        public UpdateDesignationVmValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name doldur!").MaximumLength(20).WithMessage("Max len 20!").MinimumLength(3).WithMessage("Min len 3!");
        }
    }
}


