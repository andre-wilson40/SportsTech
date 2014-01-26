using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsTech.Web.Areas.Clubs.ViewModels.Draw
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            Home = new TeamViewModel();
            Away = new TeamViewModel();
        }

        public int Id { get; set; }

        public TimeSpan ElapsedTime { get; set; }

        public TeamViewModel Home { get; set; }

        public TeamViewModel Away { get; set; }

    }

    public class DashboardViewModelValidator : AbstractValidator<DashboardViewModel>
    {
        public DashboardViewModelValidator()
        {
        }
    }
}