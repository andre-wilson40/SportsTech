using SportsTech.Domain.Services;
using SportsTech.Web.ViewModels.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SportsTech.Web.Controllers
{
    public class PageController : BaseAuthenticatedController
    {
        private readonly IClubService _clubService;

        public PageController(IClubService clubService)
        {
            _clubService = clubService;
        }

        [AllowAnonymous]
        public ActionResult SiteHeader()
        {
            var userId = Request.IsAuthenticated ? GetCurrentUser().UserProfile.Id : 0;

            var clubs = Library.AsyncHelpers.RunSync<List<Data.Model.Club>>(() => _clubService.GetAllAsync());

            var viewModel = new SiteHeaderViewModel
            {
                Clubs = clubs.Select(p => new ViewModels.Shared.KeyPairViewModel
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList()
            };

            return PartialView("_SiteHeader", viewModel);
        }
	}
}