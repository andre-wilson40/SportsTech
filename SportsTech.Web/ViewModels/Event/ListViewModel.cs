using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsTech.Web.ViewModels.Event
{
    public class ListViewModel
    {
        public ListViewModel()
        {
            Events = new List<ListItemViewModel>();
        }

        public IReadOnlyCollection<ListItemViewModel> Events { get; set; }
    }

    public class ListItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Score { get; set; }

        public DateTime EventDate { get; set; }

        public bool IsHomeGame { get; set; }
    }
}