using SportsTech.Domain.Services;
using SportsTech.Web.Areas.Clubs.ViewModels.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SportsTech.Web.Areas.Clubs.Controllers
{
    public class TeamController : SportsTech.Web.Controllers.BaseAuthenticatedController
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpPost]
        public async Task<ActionResult> DataTable(Datatables.Mvc.DataTable dataTableParams)
        {
            int recordCount = 1;
            int filteredRecordCount = 1;

            //var records = await _clubService.GetAllAsync();

            //var tableData = events.Select(p => new string[] {
            //    p.Id.ToString(),
            //    "Waihou Seniors",
            //    string.Empty,
            //    string.Empty
            //}).ToList();

            var tableData = new List<string[]>();

            return Json(new
            {
                dataTableParams.sEcho,
                iTotalRecords = recordCount,
                iTotalDisplayRecords = filteredRecordCount,
                aaData = tableData
            });
        }

        [HttpGet]
        public ActionResult List(int clubId)
        {
            return View("List");
        }

        [HttpGet]
        public ActionResult Create()
        {
            // Returns a partial for creating a game
            return View("Create");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateViewModel viewModel)
        {
            // Handle this better?
            if (!ModelState.IsValid) return Create();

            var team = AutoMapper.Mapper.Map<Data.Model.Team>(viewModel);

            if (!_teamService.CanAdd(team,null))
            {
                //ModelState.AddModelError("Name", "Validtion errors")
            }

            _teamService.Add(team);
            _teamService.SaveAnyChanges();

            return RedirectToAction("Edit", 0);
        }
	}
}