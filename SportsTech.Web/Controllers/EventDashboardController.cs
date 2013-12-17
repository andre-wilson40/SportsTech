using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsTech.Web.ViewModels.EventDashboard;

namespace SportsTech.Web.Controllers
{
    public class EventDashboardController : Controller
    {
        [HttpGet]
        [ChildActionOnly]
        public ActionResult ActivityList(int id)
        {
            var viewModel = new EventActivityListViewModel
            {
                ActivityHistory = new List<EventActivityViewModel>
                {
                    new EventActivityViewModel
                    {
                        Description = "Penalty",
                        OccuredAt = DateTime.Now,
                        User = new EventActivityUserViewModel
                        {
                            Id = 1,
                            Name = "Andre Wilson"
                        }   
                    },
                    new EventActivityViewModel
                    {
                        Description = "Try",
                        OccuredAt = DateTime.Now.AddMinutes(5),
                        User = new EventActivityUserViewModel
                        {
                            Id = 1,
                            Name = "Daniel Carter"
                        }   
                    }
                }
            };

            return View("ActivityList", viewModel);
        }
	}
}