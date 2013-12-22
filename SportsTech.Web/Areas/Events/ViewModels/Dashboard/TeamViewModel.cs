using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsTech.Web.Areas.Events.ViewModels.Dashboard
{
    public class TeamViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Score { get; set; }
    }
}