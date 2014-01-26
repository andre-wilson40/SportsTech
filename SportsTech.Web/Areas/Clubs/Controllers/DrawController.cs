using SportsTech.Data;
using SportsTech.Domain.Services;
using SportsTech.Web.Filters;
using SportsTech.Web.Models;
using SportsTech.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SportsTech.Web.Areas.Clubs.Controllers
{
    [Filters.ClubAuthorize]
    public class DrawController : SportsTech.Web.Controllers.BaseAuthenticatedController
    {
        private readonly IEventService _eventService;
        private readonly ISeasonServiceFactory _seasonServiceFactory;
        private readonly IClubSeasonFacadeService _seasonService;
        private readonly ITeamService _teamService;
        private readonly IUnitOfWork _unitOfWork;

        public DrawController(
            IUnitOfWork unitOfWork,
            IClubSeasonFacadeService clubSeasonService,
            ISeasonServiceFactory seasonServiceFactory,
            IEventService eventService,
            ITeamService teamService)
        {
            _unitOfWork = unitOfWork;
            _eventService = eventService; // DataTable M, Create, Edit
            _seasonServiceFactory = seasonServiceFactory; // List BC, Create
            _seasonService = clubSeasonService; // List BC, Create
            _teamService = teamService; // Create VM, Edit
            // SeasonRound (unitOfwork)
        }

        [HttpPost]
        public async Task<ActionResult> DataTable(int seasonId, Datatables.Mvc.DataTable dataTableParams)
        {
            int recordCount = 1;
            int filteredRecordCount = 1;

            var events = await _eventService.GetAllAsync(p => p.SeasonId == seasonId);
            var mapper = new Mapping.Draw.DataTableMap();
            var viewModels = events.Select(p => mapper.Map(p));
            var user = GetCurrentUser();

            var tableData = viewModels.Select(p => new string[] {
                p.Id.ToString(),
                p.Name,
                p.Score,                
                p.EventDate.ToString(user.UserProfile.DateFormat),
                p.IsHomeGame ? "Home" : "Away",
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
        public async Task<ActionResult> List(int id /* SeasonId */, int clubId)
        {
            var season = await _seasonServiceFactory.Create().GetByIdAsync(id);            
            var clubCompetition = await _seasonService.GetClubCompetitionAsync(season.CompetitionId);
            var clubBreadCrumb = clubCompetition.GetBreadCrumb(Url);
   
            CreateBreadCrumb(clubBreadCrumb,
                             new BreadCrumb(clubCompetition.CompetitionName, Url.Action("List", "Competition")),
                             new BreadCrumb(season.Name, Url.Action("List", "Season", new { id= season.CompetitionId })),
                             new BreadCrumb("Draw")
            );

            return View("List", id);
        }


        [HttpGet]
        public async Task<ActionResult> Create(int seasonId)
        {
            var teams = await _teamService.GetAllAsync();
            var rounds = _unitOfWork.GetRepository<Data.Model.SeasonRound>().AsQueryable().ToList();
            
            var returnUrl = Url.Action("List", new { id = seasonId });

            var mapper = new Mapping.Draw.CreateViewModelMap(teams, rounds, returnUrl);
            var viewModel = mapper.Map(new Data.Model.Event() 
            {
                EventDate = DateTime.Now,
                SeasonId = seasonId
            });

            // Returns a partial for creating a game
            return View("Create", viewModel);
        }


        [HttpPost, WebValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ViewModels.Draw.CreateViewModel viewModel)
        {
            // Handle this better?
            if (!ModelState.IsValid) return Create(viewModel.SeasonId).Result;

            var season = await _seasonServiceFactory.Create().GetByIdAsync(viewModel.SeasonId);
            var team = await _teamService.GetByIdAsync(viewModel.TeamId);

            var roundRepository = _unitOfWork.GetRepository<Data.Model.SeasonRound>();
            var round = await roundRepository.SingleOrDefaultAsync(p => p.Id == viewModel.RoundId);

            if (season == null || team == null || round == null)
            {
                return ResourceNotFound();
            }
           
            var mapper = new Mapping.Draw.CreateDataModelMap();
            var ev = mapper.Map(viewModel);
            var errorHandler = CreateModelErrorHandler();

            if (!await _eventService.CanAdd(ev, errorHandler))
            {
                return Create(viewModel.SeasonId).Result;
            }

            _eventService.Add(ev);
            _eventService.SaveAnyChanges();

            return Redirect(viewModel.ReturnUrl);
        }


        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var ev = await _eventService.GetByIdAsync(id);

            if (ev == null) return ResourceNotFound();

            var teams = await _teamService.GetAllAsync();
            var rounds = _unitOfWork.GetRepository<Data.Model.SeasonRound>().AsQueryable().ToList();
            var returnUrl = Url.Action("List", new { id = ev.SeasonId });

            var mapper = new Mapping.Draw.CreateViewModelMap(teams,rounds, returnUrl);
            var viewModel = mapper.Map(ev);

            return View("Edit", viewModel);
        }


        [HttpPost, Web.Filters.WebValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ViewModels.Draw.CreateViewModel viewModel)
        {
            // Handle this better?
            if (!ModelState.IsValid) return await Create(viewModel.SeasonId);

            var season = await _seasonServiceFactory.Create().GetByIdAsync(viewModel.SeasonId);            
            var team = await _teamService.GetByIdAsync(viewModel.TeamId);
            
            var roundRepository = _unitOfWork.GetRepository<Data.Model.SeasonRound>();
            var round = await roundRepository.SingleOrDefaultAsync(p => p.Id == viewModel.RoundId);

            var ev = await _eventService.GetByIdAsync(viewModel.Id.GetValueOrDefault());

            if (ev == null || season == null || team == null || round == null)
            {
                return ResourceNotFound();
            }

            var mapper = new Mapping.Draw.CreateDataModelMap(ev);
            mapper.Map(viewModel);
            var errorHandler = CreateModelErrorHandler();
            
            if(!await _eventService.CanAdd(ev,errorHandler)) {
                return await Create(viewModel.SeasonId);
            }

            _eventService.SaveAnyChanges();

            return Redirect(viewModel.ReturnUrl);
        }


        [HttpPost, Web.Filters.WebValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var ev = await _eventService.GetByIdAsync(id);
            if (ev == null) return ResourceNotFound();

            _eventService.Remove(ev);
            _eventService.SaveAnyChanges();

            return RedirectToAction("List", new { id = ev.SeasonId });
        }

        [HttpGet]
        public async Task<ActionResult> Dashboard(int id)
        {
            var ev = await _eventService.GetByIdAsync(id);
            if (ev == null) return ResourceNotFound();

            var viewModel = new ViewModels.Draw.DashboardViewModel();
            
            return View("Dashboard", viewModel);
        }
	}
}