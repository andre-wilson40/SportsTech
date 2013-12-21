using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsTech.Web.Areas.Events.ViewModels.Dashboard
{
    public class DashboardViewModel
    {
        public int Id { get; set; }

    }

    public class DashboardViewModelValidator : AbstractValidator<DashboardViewModel>
    {
        public DashboardViewModelValidator()
        {
        }
    }
}