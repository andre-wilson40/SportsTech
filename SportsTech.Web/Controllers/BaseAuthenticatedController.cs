using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SportsTech.Data;
using SportsTech.Data.Entity;
using SportsTech.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsTech.Web.Controllers
{
    [Authorize]
    public class BaseAuthenticatedController : Controller
    {
        /// <summary>
        /// Using Property injection so that we do not need to worry about supplying a constructor setting for this in all inherited children
        /// </summary>
        public UserManager<ApplicationUser> UserManager { get; set; }

        protected Domain.IErrorHandler CreateModelErrorHandler()
        {
            return new ModelStateAdapter(ModelState);
        }

        protected ApplicationUser GetCurrentUser()
        {
            var user = UserManager.FindByNameAsync(User.Identity.Name).Result;            

            if(user == null) throw new UnauthorizedAccessException("The current user is not available [" + User.Identity.Name + "]");
            
            return user;
        }

        protected ActionResult ResourceNotFound()
        {
            throw new ResourceNotFoundException();
        }

        protected void CreateBreadCrumb(params BreadCrumb[] breadCrumbs)
        {
            var trail = BreadCrumbTrail.Create();
            breadCrumbs.ToList().ForEach(p => trail.Add(p));

            ViewBag.Crumb = trail;
        }
    }
}