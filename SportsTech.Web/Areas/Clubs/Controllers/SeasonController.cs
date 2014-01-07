using SportsTech.Domain.Services;
using SportsTech.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SportsTech.Web.Areas.Clubs.Controllers
{
    public class SeasonController : SportsTech.Web.Controllers.BaseAuthenticatedController
    {
        private readonly IClubService _clubService;
        private readonly ISeasonServiceFactory _seasonServiceFactory;
        private readonly ICompetitionService _competitionService;

        public SeasonController(
            IClubService clubService,
            ICompetitionService competitionService,
            ISeasonServiceFactory seasonServiceFactory)
        {
            _clubService = clubService;
            _competitionService = competitionService;
            _seasonServiceFactory = seasonServiceFactory;
        }

        [HttpPost]
        public async Task<ActionResult> DataTable(int competitionId, Datatables.Mvc.DataTable dataTableParams)
        {
            var seasonService = await _seasonServiceFactory.CreateAsync(competitionId);
            var records = await seasonService.GetAllAsync();

            int recordCount = records.Count;

            if (!string.IsNullOrWhiteSpace(dataTableParams.sSearch))
            {
                records = await seasonService.GetAllAsync(p => p.Name.Contains(dataTableParams.sSearch));
            }

            int filteredRecordCount = records.Count;

            var tableData = records.Select(p => new string[] {
                p.Id.ToString(),
                p.Name,
                p.From.ToString(),
                p.To.ToString(),
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
        public async Task<ActionResult> List(int id, int clubId)
        {
            var club = await _clubService.GetByIdAsync(clubId);
            var competition = await _competitionService.GetByIdAsync(id);
            var clubBreadCrumb = new ClubAdapter(club).GetBreadCrumb(Url);

            CreateBreadCrumb(clubBreadCrumb,
                             new BreadCrumb("Competitions", Url.Action("List", "Competition")),
                             new BreadCrumb(competition.Name)
            );
            

            return View("List", id);
        }
	}
}