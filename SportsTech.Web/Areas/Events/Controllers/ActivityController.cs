using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsTech.Web.Areas.Events.ViewModels.Activity;

namespace SportsTech.Web.Areas.Events.Controllers
{
    public class ActivityController : Controller
    {
        [HttpGet]
        [ChildActionOnly]
        public ActionResult List(int eventId)
        {
            var viewModel = new ListViewModel
            {
                ActivityHistory = new List<ListItemViewModel>
                {
                    new ListItemViewModel
                    {
                        Description = "Penalty",
                        OccuredAt = DateTime.Now,
                        User = new PersonViewModel
                        {
                            Id = 1,
                            Name = "Andre Wilson"
                        }   
                    },
                    new ListItemViewModel
                    {
                        Description = "Try",
                        OccuredAt = DateTime.Now.AddMinutes(5),
                        User = new PersonViewModel
                        {
                            Id = 1,
                            Name = "Daniel Carter"
                        }   
                    }
                }
            };

            return View("List", viewModel);
        }
	}
}