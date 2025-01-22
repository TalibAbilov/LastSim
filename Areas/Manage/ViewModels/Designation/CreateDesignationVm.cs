﻿using FluentValidation;

namespace KiderApp.Areas.Manage.ViewModels.Designation
{
    public class CreateDesignationVm
    {
        public string Name { get; set; }
    }
    public class CreateDesignationVmValidator : AbstractValidator<CreateDesignationVm>
    {
        public CreateDesignationVmValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Name doldur!").MaximumLength(20).WithMessage("Max len 20!").MinimumLength(3).WithMessage("Min len 3!");
        }
    }
}
