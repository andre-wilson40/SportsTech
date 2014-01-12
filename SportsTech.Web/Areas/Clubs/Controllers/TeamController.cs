using SportsTech.Domain.Services;
using SportsTech.Web.Areas.Clubs.Models;
using SportsTech.Web.Areas.Clubs.ViewModels.Team;
using SportsTech.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SportsTech.Web.Areas.Clubs.Controllers
{
    [ClubAuthorize]
    public class TeamController : SportsTech.Web.Controllers.BaseAuthenticatedController
    {
        private readonly ITeamService _teamService;
        private readonly IClubService _clubService;

        public TeamController(
            IClubService clubService,
            ITeamService teamService)
        {
            _clubService = clubService;
            _teamService = teamService;
        }

        [HttpPost]
        public async Task<ActionResult> DataTable(Datatables.Mvc.DataTable dataTableParams)
        {
            var records = await _teamService.GetAllAsync();
            int recordCount = records.Count;
            
            if(!string.IsNullOrWhiteSpace(dataTableParams.sSearch))
            {
                records = await _teamService.GetAllAsync(p => p.Name.Contains(dataTableParams.sSearch));
            }

            int filteredRecordCount = records.Count;

            var tableData = records.Select(p => new string[] {
                p.Id.ToString(),
                p.Name,
                string.Empty,
                string.Empty
            }).ToList();

            return Json(new
            {
                dataTableParams.sEcho,
                iTotalRecords = recordCount,
                iTotalDisplayRecords = filteredRecordCount,
                aaData = tableData
            });
        }

        [HttpGet]
        public async Task<ActionResult> List(int clubId)
        {
            var club = await _clubService.GetByIdAsync(clubId);            
            var clubBreadCrumb = new ClubAdapter(club).GetBreadCrumb(Url);

            CreateBreadCrumb(clubBreadCrumb,
                             new BreadCrumb("Teams")                             
            );
            
            return View("List");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var team = await _teamService.GetByIdAsync(id);
            var viewModel = AutoMapper.Mapper.Map<CreateViewModel>(team);

            return View("Edit", viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int clubId, CreateViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var team = await _teamService.GetByIdAsync(viewModel.Id.GetValueOrDefault());
            AutoMapper.Mapper.Map<CreateViewModel, Data.Model.Team>(viewModel, team);

            var errorHandler = CreateModelErrorHandler();

            if (! await _teamService.CanAdd(team,errorHandler))
            {
                 return View(viewModel);
            }

            _teamService.SaveAnyChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Create()
        {
            // Returns a partial for creating a game
            return View("Create");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int clubId, CreateViewModel viewModel)
        {
            // Handle this better?
            if (!ModelState.IsValid) return Create();

            var team = AutoMapper.Mapper.Map<Data.Model.Team>(viewModel);
            var errorHandler = CreateModelErrorHandler();

            if (! await _teamService.CanAdd(team,errorHandler))
            {
                return Create();
            }

            _teamService.Add(team);
            _teamService.SaveAnyChanges();

            return RedirectToAction("List");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var team = await _teamService.GetByIdAsync(id);

            _teamService.Remove(team);
            _teamService.SaveAnyChanges();

            return RedirectToAction("List");
        }
	}
}