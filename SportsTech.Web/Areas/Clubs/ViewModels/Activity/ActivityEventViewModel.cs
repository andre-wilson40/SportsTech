using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsTech.Web.Areas.Clubs.ViewModels.Activity
{
    public class ActivityEventViewModel
    {
        public ActivityEventViewModel()
        {
            Occurances = new List<ActivityEventItemViewModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<ActivityEventItemViewModel> Occurances { get; set; }
    }

    public class ActivityEventItemViewModel
    {
        public int Id { get; set;}
    }
}