using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SportsTech.Web.Areas.Clubs.ViewModels.Competition
{
    [Validator(typeof(CreateViewModelValidator))]
    public class CreateViewModel
    {
        public int? Id { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }
    }

    public class CreateViewModelValidator : AbstractValidator<CreateViewModel>
    {
        public CreateViewModelValidator()
        {
            RuleFor(p => p.Name).NotEmpty().Length(1, 50);
        }
    }
}