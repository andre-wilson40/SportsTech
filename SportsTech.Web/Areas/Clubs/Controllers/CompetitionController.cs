﻿using SportsTech.Domain.Services;
using SportsTech.Web.Areas.Clubs.Filters;
using SportsTech.Web.Areas.Clubs.ViewModels.Competition;
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
    public class CompetitionController : SportsTech.Web.Controllers.BaseAuthenticatedController
    {
        private readonly ICompetitionService _competitionService;
        private readonly IClubService _clubService;

        public CompetitionController(
            IClubService clubService,
            ICompetitionService competitionService)
        {
            _competitionService = competitionService;
            _clubService = clubService;
        }

        [HttpPost]
        public async Task<ActionResult> DataTable(Datatables.Mvc.DataTable dataTableParams)
        {
            var records = await _competitionService.GetAllAsync();
            int recordCount = records.Count;

            if (!string.IsNullOrWhiteSpace(dataTableParams.sSearch))
            {
                records = await _competitionService.GetAllAsync(p => p.Name.Contains(dataTableParams.sSearch));
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

            CreateBreadCrumb(clubBreadCrumb, new BreadCrumb("Competitions")                             
            );
            
            return View("List");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost, Web.Filters.WebValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int clubId, CreateViewModel viewModel)
        {
            // Handle this better?
            if (!ModelState.IsValid) return Create();

            var competition = AutoMapper.Mapper.Map<Data.Model.Competition>(viewModel);
            var errorHandler = CreateModelErrorHandler();

            if (!await _competitionService.CanAdd(competition, errorHandler))
            {
                return Create();
            }

            _competitionService.Add(competition);
            _competitionService.SaveAnyChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var team = await _competitionService.GetByIdAsync(id);
            var viewModel = AutoMapper.Mapper.Map<CreateViewModel>(team);

            return View("Edit", viewModel);
        }

        [HttpPost, Web.Filters.WebValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int clubId, CreateViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var team = await _competitionService.GetByIdAsync(viewModel.Id.GetValueOrDefault());
            AutoMapper.Mapper.Map<CreateViewModel, Data.Model.Competition>(viewModel, team);

            var errorHandler = CreateModelErrorHandler();

            if (!await _competitionService.CanAdd(team, errorHandler))
            {
                return View(viewModel);
            }

            _competitionService.SaveAnyChanges();

            return RedirectToAction("List");
        }

        [HttpPost, Web.Filters.WebValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var competition = await _competitionService.GetByIdAsync(id);

            _competitionService.Remove(competition);
            _competitionService.SaveAnyChanges();

            return RedirectToAction("List");
        }
	}
}