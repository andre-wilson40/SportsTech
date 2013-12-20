using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using FluentValidation;
using FluentValidation.Attributes;

namespace SportsTech.Web.Areas.Events.ViewModels.Event
{
    [Validator(typeof(CreateEventViewModelValidator))]
    public class CreateViewModel
    {
        [DisplayName("Against")]
        public string Against { get; set; }

        [DisplayName("At home")]
        public bool IsHomeGame { get; set; }

        [DisplayName("Referee")]
        public string Referee { get; set; }

        [DisplayName("Game day")]        
        public DateTime EventDate { get; set; }
    }

    public class CreateEventViewModelValidator : AbstractValidator<CreateViewModel>
    {
        public CreateEventViewModelValidator()
        {
            RuleFor(vm => vm.EventDate).NotEmpty().WithMessage("Every event must have a date it is occuring on");
            RuleFor(vm => vm.Against).NotEmpty().WithMessage("Please specify the opposition for this event");
        }
    }
}