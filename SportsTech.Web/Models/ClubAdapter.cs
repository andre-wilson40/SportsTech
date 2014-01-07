using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsTech.Web.Models
{
    public class ClubAdapter
    {
        public string Name { get; private set; }

        public ClubAdapter(Data.Model.Club club)
        {
            Name = club.Name;
        }

        public BreadCrumb GetBreadCrumb(System.Web.Mvc.UrlHelper urlHelper)
        {
            return new BreadCrumb(Name, urlHelper.Action("Index", "Club"));
        }
    }
}