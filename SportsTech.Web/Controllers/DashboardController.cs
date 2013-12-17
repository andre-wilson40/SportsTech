using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsTech.Web.Controllers
{
    public class DashboardController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            //return View();
            return RedirectToAction("List", "Event");
        }
	}
}