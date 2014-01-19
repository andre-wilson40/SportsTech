using AutoMapper;
using SportsTech.Domain.Services;
using SportsTech.Web.Areas.Clubs.Filters;
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
    public class TeamCompetitionController : SportsTech.Web.Controllers.BaseAuthenticatedController
    {
        private readonly IClubService _clubService;
        private readonly ITeamCompetitionService _teamCompetitionService;

        public TeamCompetitionController(
            IClubService clubService,
            ITeamCompetitionService teamCompetitionService)
        {
            _clubService = clubService;
            _teamCompetitionService = teamCompetitionService;
        }

        [HttpPost]
        public async Task<ActionResult> DataTable(int teamId, Datatables.Mvc.DataTable dataTableParams)
        {
            // Return all the competitions this team is involved with
            var records = await _teamCompetitionService.GetCompetitionsAsync(teamId);
            int recordCount = records.Count;

            if (!string.IsNullOrWhiteSpace(dataTableParams.sSearch))
            {
                records = await _teamCompetitionService.GetCompetitionsAsync(
                    teamId, 
                    p => p.Season.Competition.Name.Contains(dataTableParams.sSearch) ||
                         p.Season.Name.Contains(dataTableParams.sSearch));
            }

            int filteredRecordCount = records.Count;

            // TODO:  this is fairly inefficient because of the fact that EF will do another query for the season for each competition due to it's lazy
            //        loading, rather than returning the entire set immediately back.  It would be better to have the Service layer return us the required object?
            var tableData = records.Select(p => new string[] {
                p.Id.ToString(),
                p.Season.Competition.Name,
                p.Season.Name,
                string.Empty // Delete
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
        public async Task<ActionResult> List(int id /* teamId */, int clubId)
        {
            var club = await _clubService.GetByIdAsync(clubId);
            var team = await _teamCompetitionService.GetTeam(id);
            var clubBreadCrumb = new ClubAdapter(club).GetBreadCrumb(Url);
            var teamBreadCrumb = new BreadCrumb(team.Name, Url.Action("List", "Team"));
            
            // Club / Team / Competions
            CreateBreadCrumb(clubBreadCrumb,
                             teamBreadCrumb,
                             new BreadCrumb("Competitions")                             
            );
            
            return View("List", id);
        }

        [HttpGet]
        public async Task<ActionResult> Register(int teamId, int clubId)
        {
            var club = await _clubService.GetByIdAsync(clubId);
            var team = await _teamCompetitionService.GetTeam(teamId);
            var clubBreadCrumb = new ClubAdapter(club).GetBreadCrumb(Url);
            var teamBreadCrumb = new BreadCrumb(team.Name, Url.Action("List", "Team"));

            // Club / Team / Competions
            CreateBreadCrumb(clubBreadCrumb,
                             teamBreadCrumb,
                             new BreadCrumb("Registration")
            );

            // TODO:  Inject mapping layer to controller
            var registration = new Data.Model.CompetitionRegistration
                {
                    Team = team,
                    TeamId = team.Id
                };

            var viewModel = new Mapping.TeamCompetition.RegisterMap(_teamCompetitionService).Map(registration);

            return View("Register", viewModel);
        }

        [HttpPost, Web.Filters.WebValidateAntiForgeryToken]
        public async Task<ActionResult> Register(int clubId, Areas.Clubs.ViewModels.TeamCompetition.RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid) return await Register(viewModel.TeamId, clubId);

            var team = _teamCompetitionService.GetTeam(viewModel.TeamId);
            var registration = Mapper.Map<Data.Model.CompetitionRegistration>(viewModel);

            var errorHandler = CreateModelErrorHandler();
            if (!await _teamCompetitionService.CanAdd(registration, errorHandler))
            {
                return await Register(viewModel.TeamId, clubId);
            }

            _teamCompetitionService.Add(registration);
            _teamCompetitionService.SaveAnyChanges();

            return RedirectToAction("List", new { id = viewModel.TeamId });
        }

        [HttpPost, Web.Filters.WebValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var registration = await _teamCompetitionService.GetByIdAsync(id);

            _teamCompetitionService.Remove(registration);
            _teamCompetitionService.SaveAnyChanges();

            return RedirectToAction("List", new { id = registration.TeamId });
        }
	}
}