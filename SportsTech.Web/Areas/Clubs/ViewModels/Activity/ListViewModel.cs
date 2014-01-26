using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsTech.Web.Areas.Clubs.ViewModels.Activity
{
    public class ListViewModel
    {
        public ListViewModel()
        {
            ActivityHistory = new List<ListItemViewModel>();
        }

        public List<ListItemViewModel> ActivityHistory { get; set; }
    }
}