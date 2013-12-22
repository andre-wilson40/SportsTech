using SportsTech.Web.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsTech.Web.Controllers
{
    public class DialogController : Controller
    {
        [ChildActionOnly]
        public ActionResult Delete(DeleteDialogViewModel dialog)
        {
            return PartialView("_DeleteDialog", dialog);
        }
	}
}