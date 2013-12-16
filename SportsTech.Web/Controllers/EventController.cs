using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsTech.Web.ViewModels.Event;
using SportsTech.Web.Filters;
using System.Threading.Tasks;

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
        public ActionResult Create()
        {
            var viewModel = new CreateViewModel();

            // Returns a partial for creating a game
            return View("Create", viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateViewModel viewModel)
        {
            // Handle this better?
            if (!ModelState.IsValid) return PartialView("_Create", viewModel);

            // TODO:  Persist model to database ensuring the db operations are async
            
            return RedirectToAction("Edit", 0);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View("Edit");
        }

        [HttpGet]
        public async Task<ActionResult> Dashboard(int id)
        {
            // Return the view that contains the full event details for this game
            return View("Dashboard");
        }
	}
}