using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsTech.Web.Areas.Clubs.ViewModels.Activity
{
    public class ListItemViewModel
    {
        public ListItemViewModel()
        {
            User = new PersonViewModel();
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime OccuredAt { get; set; }

        public PersonViewModel User { get; set; }
    }
}