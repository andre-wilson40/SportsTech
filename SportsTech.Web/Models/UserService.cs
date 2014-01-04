using Microsoft.AspNet.Identity;
using SportsTech.Data.Entity;
using SportsTech.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SportsTech.Web.Models
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HttpContextBase _httpContext;

        public UserService(UserManager<ApplicationUser> userManager, HttpContextBase httpContext)
        {
            _userManager = userManager;
            _httpContext = httpContext;
        }

        public Data.Model.UserProfile CurrentUserProfile()
        {
            var userName = _httpContext.User.Identity.Name.ToString();

            if (string.IsNullOrWhiteSpace(userName)) return new Data.Model.UserProfile();

            var user = _userManager.FindByName(userName);

            return user.UserProfile;
        }
    }
}