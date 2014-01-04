using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsTech.Web.Areas.Clubs.Controllers
{
    public class TeamController : Controller
    {
        public ActionResult List(int clubId)
        {
            return View();
        }
	}
}