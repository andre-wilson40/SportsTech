using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentValidation;
using FluentValidation.Attributes;

namespace SportsTech.Web.Areas.Clubs.ViewModels.TeamCompetition
{
    [Validator(typeof(RegisterViewModelValidator))]
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            Seasons = new List<SelectListItem>();
        }

        public int TeamId { get; set; }

        public List<SelectListItem> Seasons { get; set; }
        
        [Display(Name = "Available competitions")]
        public int SeasonId { get; set; }
    }

    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(p => p.SeasonId).NotEmpty().WithLocalizedMessage(() => "Please select a competition season to register in");
        }
    }
}