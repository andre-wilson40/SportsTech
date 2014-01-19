using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using FluentValidation;
using FluentValidation.Attributes;
using System.Web.Mvc;

namespace SportsTech.Web.Areas.Clubs.ViewModels.Draw
{
    [Validator(typeof(CreateEventViewModelValidator))]
    public class CreateViewModel
    {
        public CreateViewModel()
        {
            Teams = new List<SelectListItem>();
        }

        public int? Id { get; set; }

        public List<SelectListItem> Teams { get; set; }
        [DisplayName("Team")]
        public int TeamId { get; set; }

        [DisplayName("Against")]
        public string Against { get; set; }

        [DisplayName("At home")]
        public bool IsHomeGame { get; set; }

        [DisplayName("Referee")]
        public string Referee { get; set; }

        [DisplayName("Game day")]        
        public DateTime EventDate { get; set; }

        public int SeasonId { get; set; }

        public string ReturnUrl { get; set; }
    }

    public class CreateEventViewModelValidator : AbstractValidator<CreateViewModel>
    {
        public CreateEventViewModelValidator()
        {
            RuleFor(vm => vm.EventDate).NotEmpty().WithMessage("Every event must have a date it is occuring on");
            RuleFor(vm => vm.Against).NotEmpty().WithMessage("Please specify the opposition for this event");
            RuleFor(vm => vm.SeasonId).NotEmpty();
        }
    }
}