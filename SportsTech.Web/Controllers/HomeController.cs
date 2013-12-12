using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsTech.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your technical assistant";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us";
            
            return View();
        }
    }
}