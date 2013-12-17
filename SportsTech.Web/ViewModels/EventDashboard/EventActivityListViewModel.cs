using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsTech.Web.ViewModels.EventDashboard
{
    public class EventActivityListViewModel
    {
        public EventActivityListViewModel()
        {
            ActivityHistory = new List<EventActivityViewModel>();
        }

        public List<EventActivityViewModel> ActivityHistory { get; set; }
    }

    public class EventActivityViewModel
    {
        public EventActivityViewModel()
        {
            User = new EventActivityUserViewModel();
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime OccuredAt { get; set; }

        public EventActivityUserViewModel User { get; set; }
    }

    public class EventActivityUserViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}