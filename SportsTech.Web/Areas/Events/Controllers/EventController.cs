//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using SportsTech.Web.Filters;
//using System.Threading.Tasks;
//using SportsTech.Data;
//using SportsTech.Web.Controllers;
//using SportsTech.Domain.Services.Core;
//using SportsTech.Domain.Services;

//namespace SportsTech.Web.Areas.Events.Controllers
//{
//    public class EventController : BaseAuthenticatedController
//    {
//        private IEventService _eventService;

//        public EventController(IEventService eventService)
//        {
//            _eventService = eventService;
//        }

//        [HttpPost]
//        public async Task<ActionResult> DataTable(Datatables.Mvc.DataTable dataTableParams) 
//        {
//            int recordCount = 1;
//            int filteredRecordCount = 1;

//            var events = await _eventService.GetAllAsync();

//            var tableData = events.Select(p => new string[] {
//                p.Id.ToString(),
//                "Waihou vs Cobras",
//                "26 - 5",
//                "home",
//                p.EventDate.ToString(),
//                string.Empty,
//                string.Empty
//            }).ToList();

//            return Json(new
//            {
//                dataTableParams.sEcho,
//                iTotalRecords = recordCount,
//                iTotalDisplayRecords = filteredRecordCount,
//                aaData =  tableData
//            });
//        }

//        [HttpGet]
//        public ActionResult List()
//        {        
//            return View("List");
//        }

//        [HttpGet]
//        public ActionResult Create()
//        {
//            var viewModel = new CreateViewModel();

//            // Returns a partial for creating a game
//            return View("Create", viewModel);
//        }

//        [HttpPost, Web.Filters.WebValidateAntiForgeryToken]
//        public ActionResult Create(CreateViewModel viewModel)
//        {
//            // Handle this better?
//            if (!ModelState.IsValid) return PartialView("_Create", viewModel);

//            // TODO:  Persist model to database ensuring the db operations are async
            
//            return RedirectToAction("Edit", 0);
//        }

//        [HttpGet]
//        public ActionResult Edit(int id)
//        {
//            var viewModel = new CreateViewModel
//            {
//                Against = "Waihou",
//                EventDate = DateTime.Now,
//                IsHomeGame = true,
//                Referee = "Steve Walsh"
//            };

//            return View("Edit", viewModel);
//        }

//        [HttpPost, Web.Filters.WebValidateAntiForgeryToken]
//        public ActionResult Edit(CreateViewModel viewModel)
//        {
//            return RedirectToAction("List");
//        }

//        [HttpPost, Web.Filters.WebValidateAntiForgeryToken]        
//        public ActionResult Delete(int id)
//        {
//            return RedirectToAction("List");
//        }
//    }
//}