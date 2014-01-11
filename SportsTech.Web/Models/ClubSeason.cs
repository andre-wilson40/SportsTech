using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsTech.Web.Models
{
    public class ClubCompetition
    {
        public string CompetitionName { get; private set; }
        private ClubAdapter Club { get; set; }

        public ClubCompetition(Data.Model.Competition competition)
        {
            CompetitionName = competition.Name;
            Club = new ClubAdapter(competition.Club);
        }

        public BreadCrumb GetBreadCrumb(System.Web.Mvc.UrlHelper urlHelper)
        {
            return Club.GetBreadCrumb(urlHelper);
        }
    }
}