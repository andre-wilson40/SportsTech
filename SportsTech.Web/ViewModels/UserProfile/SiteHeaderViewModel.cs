using SportsTech.Web.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsTech.Web.ViewModels.UserProfile
{
    public class SiteHeaderViewModel
    {
        public SiteHeaderViewModel()
        {
            Clubs = new List<KeyPairViewModel>();
        }

        public ICollection<KeyPairViewModel> Clubs { get; set; }
    }
}