using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsTech.Web.Areas.Events.ViewModels.Dashboard
{
    public class DashboardViewModel
    {
        // Event details

    }

    public class DashboardViewModelValidator : AbstractValidator<DashboardViewModel>
    {
        public DashboardViewModelValidator()
        {
        }
    }
}