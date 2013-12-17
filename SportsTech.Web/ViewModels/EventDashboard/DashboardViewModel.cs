using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsTech.Web.ViewModels.EventDashboard
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