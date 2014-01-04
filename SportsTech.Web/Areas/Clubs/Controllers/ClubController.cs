﻿using AutoMapper;
using SportsTech.Domain.Services;
using SportsTech.Web.Areas.Clubs.ViewModels;
using SportsTech.Web.Areas.Clubs.ViewModels.Club;
using SportsTech.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SportsTech.Web.Areas.Clubs.Controllers
{
    public class ClubController : BaseAuthenticatedController
    {
        private readonly IClubService _clubService;

        public ClubController(IClubService clubService)
        {
            _clubService = clubService;
        }

        [AllowAnonymous]
        public ActionResult SiteHeader()
        {
            var userId = GetCurrentUser().UserProfile.Id;

            var viewModel = new SiteHeaderViewModel
                {
                    ClubCount = _clubService.AffliatedClubCount().Result
                };

            return PartialView("_SiteHeader", viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> DataTable(Datatables.Mvc.DataTable dataTableParams)
        {
            int recordCount = 1;
            int filteredRecordCount = 1;

            var records = await _clubService.GetAllAsync();

            //var tableData = events.Select(p => new string[] {
            //    p.Id.ToString(),
            //    "Waihou vs Cobras",
            //    "26 - 5",
            //    "home",
            //    p.EventDate.ToString(),
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

        /// <summary>
        /// Shows information and details on clubs and how to create and use them.  A sort of help
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Standard view for showing items in a table list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult List()
        {
            return View("List");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateViewModel viewModel)
        {
            if (!ModelState.IsValid) return Create();

            var model = new Data.Model.Club();
            Mapper.Map(viewModel, model);
            
            var user = GetCurrentUser();
            user.UserProfile.Clubs.Add(model);

            _clubService.SaveAnyChanges();
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            return RedirectToAction("List");
        }
	}
}