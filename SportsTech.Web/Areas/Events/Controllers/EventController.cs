using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsTech.Web.Filters;
using System.Threading.Tasks;
using SportsTech.Web.Areas.Events.ViewModels.Event;

namespace SportsTech.Web.Areas.Events.Controllers
{
    public class EventController : Controller
    {
        [HttpPost]
        public ActionResult DataTable(Datatables.Mvc.DataTable dataTableParams) 
        {
            int recordCount = 1;
            int filteredRecordCount = 1;

            var tableData = new string[] {
                "1",
                "Waihou vs Cobras",
                "26 - 5",
                "Home",
                DateTime.Now.ToString(),
                string.Empty, // Dashboard
                string.Empty // Delete
            };

            return Json(new
            {
                dataTableParams.sEcho,
                iTotalRecords = recordCount,
                iTotalDisplayRecords = filteredRecordCount,
                aaData = new List<string[]> { tableData }
            });
        }

        [HttpGet]
        public async Task<ActionResult> List()
        {        
            return View("List");
        }

        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new CreateViewModel();

            // Returns a partial for creating a game
            return View("Create", viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
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
            var viewModel = new CreateViewModel
            {
                Against = "Waihou",
                EventDate = DateTime.Now,
                IsHomeGame = true,
                Referee = "Steve Walsh"
            };

            return View("Edit", viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(CreateViewModel viewModel)
        {
            return RedirectToAction("List");
        }

        [HttpPost, ValidateAntiForgeryToken]        
        public ActionResult Delete(int id)
        {
            return RedirectToAction("List");
        }
	}
}