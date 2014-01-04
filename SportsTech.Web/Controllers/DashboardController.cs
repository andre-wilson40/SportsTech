using SportsTech.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SportsTech.Web.Controllers
{
    public class DashboardController : BaseAuthenticatedController
    {
        private readonly IClubService _clubService;

        public DashboardController(IClubService clubService)
        {
            _clubService = clubService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }
	}
}