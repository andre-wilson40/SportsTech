using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsTech.Web.ViewModels.Event;
using SportsTech.Web.Filters;

namespace SportsTech.Web.Controllers
{
    public class EventController : Controller
    {
        //
        // GET: /Event/
        [ChildActionOnly]
        public ActionResult List()
        {
            var ev = new List<ListItemViewModel>();
            ev.Add(new ListItemViewModel
                {
                    Id = 0,
                    EventDate = DateTime.Now,
                    IsHomeGame = true,
                    Name = "Waihou vs Cobras",
                    Score = "26 - 5"
                });

            var viewModel = new ListViewModel
            {
                Events = ev
            };

            return PartialView("_List", viewModel);
        }

        [HttpGet]
        [HttpAjax]
        public ActionResult Create()
        {
            var viewModel = new CreateViewModel();

            // Returns a partial for creating a game
            return PartialView("_Create", viewModel);
        }

        [HttpPost]
        public ActionResult CreateEvent(CreateViewModel viewModel)
        {
            // Handle this better?
            if (!ModelState.IsValid) return PartialView("_Create", viewModel);

            // TODO:  Persist model to database

            return RedirectToAction("Edit", 0);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View("Edit");
        }
	}
}