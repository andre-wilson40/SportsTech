using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsTech.Web.Areas.Events.ViewModels.Activity;
using SportsTech.Web.Controllers;
using SportsTech.Data;

namespace SportsTech.Web.Areas.Events.Controllers
{
    public class ActivityController : BaseAuthenticatedController 
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

            return PartialView("_List", viewModel);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Available(int eventId)
        {
            var viewModel = new AvailableViewModel
            {
                Events = new List<ActivityEventViewModel>
                {
                    new ActivityEventViewModel { Id = 1, Name = "Penalty" },
                    new ActivityEventViewModel { Id = 2, Name = "Ruck" }
                }
            };
            
            return PartialView("_Available", viewModel);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult SystemDefined(int eventId)
        {
            return PartialView("_SystemDefined", eventId);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Stoppages(int eventId)
        {
            return PartialView("_Stoppages", eventId);
        }
	}
}