using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsTech.Web.Areas.Events.ViewModels.Activity
{
    public class AvailableViewModel
    {
        public AvailableViewModel()
        {
            Events = new List<ActivityEventViewModel>();
        }

        public List<ActivityEventViewModel> Events { get; set; }
    }
}