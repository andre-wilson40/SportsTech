using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsTech.Web.Areas.Clubs.ViewModels.Club
{
    [Validator(typeof(CreateViewModelValidator))]
    public class CreateViewModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }
    }

    public class CreateViewModelValidator : AbstractValidator<CreateViewModel>
    {
        public CreateViewModelValidator()
        {
            RuleFor(vm => vm.Name).NotEmpty().Length(1, 50);
        }
    }
}