﻿using SportsTech.Domain.Services;
using SportsTech.Web.Areas.Clubs.Filters;
using SportsTech.Web.Areas.Clubs.ViewModels.Season;
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
    [ClubAuthorize]
    public class SeasonController : SportsTech.Web.Controllers.BaseAuthenticatedController
    {
        private readonly ISeasonServiceFactory _seasonServiceFactory;   
        private readonly IClubSeasonFacadeService _clubSeasonService;

        public SeasonController(
            IClubSeasonFacadeService clubSeasonService,
            ISeasonServiceFactory seasonServiceFactory)
        {
            _clubSeasonService = clubSeasonService;
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
            var user = GetCurrentUser();

            var tableData = records.Select(p => new string[] {
                p.Id.ToString(),
                p.Name,
                p.From.ToString(user.UserProfile.DateFormat),
                p.To.ToString(user.UserProfile.DateFormat),
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
            var clubCompetition = await _clubSeasonService.GetClubCompetitionAsync(id);
            var clubBreadCrumb = clubCompetition.GetBreadCrumb(Url);
            
            CreateBreadCrumb(clubBreadCrumb,
                             new BreadCrumb(clubCompetition.CompetitionName, Url.Action("List", "Competition")),
                             new BreadCrumb("Seasons")
            );
            
            return View("List", id);
        }


        [HttpGet]
        public ActionResult Create(int competitionId)
        {
            return View("Create", new CreateViewModel 
            { 
                CompetitionId = competitionId,
                From = new DateTime(DateTime.Now.Year, 3, 1),
                To = new DateTime(DateTime.Now.Year, 8, 1),
                ReturnUrl = Url.Action("List", new { id = competitionId })
            });
        }


        [HttpPost, Web.Filters.WebValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int clubId, CreateViewModel viewModel)
        {
            if (!ModelState.IsValid) return Create(viewModel.CompetitionId);

            var season = AutoMapper.Mapper.Map<Data.Model.Season>(viewModel);
            var errorHandler = CreateModelErrorHandler();
            var seasonService = await _seasonServiceFactory.CreateAsync(season.CompetitionId);
            
            if (!await seasonService.CanAdd(season, errorHandler))
            {
                return Create(viewModel.CompetitionId);
            }

            seasonService.Add(season);
            seasonService.SaveAnyChanges();

            return Redirect(viewModel.ReturnUrl);
        }


        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var seasonService = _seasonServiceFactory.Create();
            var season = await seasonService.GetByIdAsync(id);

            var viewModel = AutoMapper.Mapper.Map<CreateViewModel>(season);
            viewModel.ReturnUrl = Url.Action("List", new { id = season.CompetitionId });

            return View("Edit", viewModel);
        }


        [HttpPost, Web.Filters.WebValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int clubId, CreateViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var seasonService = _seasonServiceFactory.Create();
            var season = await seasonService.GetByIdAsync(viewModel.Id.GetValueOrDefault());
            AutoMapper.Mapper.Map<CreateViewModel, Data.Model.Season>(viewModel, season);

            var errorHandler = CreateModelErrorHandler();

            if (!await seasonService.CanAdd(season, errorHandler))
            {
                return View(viewModel);
            }

            seasonService.SaveAnyChanges();

            return RedirectToAction("List", new { id = season.CompetitionId });
        }


        [HttpPost, Web.Filters.WebValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var seasonService =  _seasonServiceFactory.Create();
            var season = await seasonService.GetByIdAsync(id);

            seasonService.Remove(season);
            seasonService.SaveAnyChanges();

            return RedirectToAction("List", new { id=season.CompetitionId });
        }
	}
}