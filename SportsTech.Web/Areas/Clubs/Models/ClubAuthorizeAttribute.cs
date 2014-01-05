using Microsoft.AspNet.Identity;
using SportsTech.Data.Entity;
using SportsTech.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace SportsTech.Web.Areas.Clubs.Models
{
    /// <summary>
    /// http://hackwebwith.net/post/How-to-use-the-ASPNET-MVC-5-Filter-Overrides-Feature#.UsjsI7Qsy9U
    /// </summary>
    public class OverrideClubAuthorizeAttribute : AuthorizeAttribute, IOverrideFilter
	{
	    public Type FiltersToOverride
	    {
            get { return typeof(IAuthorizationFilter); }
	    }
	}

    public class ClubAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly int _userId;
        private readonly Data.Model.Club _club;

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated) return;

            var userName = filterContext.HttpContext.User.Identity.Name;
            var clubId = Int32.Parse(filterContext.RequestContext.RouteData.Values["clubId"].ToString());
            var userService = DependencyResolver.Current.GetService<UserManager<ApplicationUser>>();
            var user = userService.FindByName(userName);

            if (user == null)
                throw new UnauthorizedAccessException("The user is currently not logged on [" + userName + "]");

            if(user.UserProfile.Clubs.All(p => p.Id != clubId))
                throw new UnauthorizedAccessException("The user does not have access to this club [" + userName + "]");
        }
    }
}