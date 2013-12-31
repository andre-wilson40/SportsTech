using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsTech.Web.Areas.Events.ViewModels.Dashboard;
using SportsTech.Web.Controllers;

namespace SportsTech.Web.Areas.Events.Controllers
{
    public class DashboardController : BaseAuthenticatedController
    {
        [HttpGet]
        public ActionResult View(int id)
        {
            var viewModel = new DashboardViewModel
            {
                Id = id,
                Home = new TeamViewModel
                {
                    Id = 1,
                    Name = "Waihou",
                    Score = 10
                },
                Away = new TeamViewModel
                {
                    Id = 2,
                    Name = "Cobras",
                    Score = 5
                },
                ElapsedTime = DateTime.Now.AddMinutes(40).AddSeconds(10).Subtract(DateTime.Now)
            };

            return View("View", viewModel);
        }
	}
}