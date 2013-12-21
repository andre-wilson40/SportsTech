using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsTech.Web.Areas.Events.Controllers
{
    public class DashboardController : Controller
    {
        [HttpGet]
        public ActionResult View(int id)
        {
            var viewModel = new SportsTech.Web.Areas.Events.ViewModels.Dashboard.DashboardViewModel
            {
                Id = id
            };

            return View("View", viewModel);
        }
	}
}