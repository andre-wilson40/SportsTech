using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsTech.Web.Models
{
    public class BreadCrumb
    {
        public string Name { get; private set; }
        public string Url { get; private set; }

        public BreadCrumb(string name) : this(name, string.Empty)
        {

        }

        public BreadCrumb(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(Url))
            {
                var tagBuilder = new TagBuilder("a");
                tagBuilder.Attributes.Add("href", Url);
                tagBuilder.InnerHtml = Name;

                return tagBuilder.ToString();
            }

            return Name;
        }
    }

    public class BreadCrumbTrail
    {
        public ICollection<BreadCrumb> BreadCrumbs { get; private set; }

        protected BreadCrumbTrail()
        {
            BreadCrumbs = new HashSet<BreadCrumb>();
        }

        public static BreadCrumbTrail Create()
        {
            return new BreadCrumbTrail();
        }

        public static bool HasTrail(BreadCrumbTrail trail) {
            return trail != null && trail.BreadCrumbs.Any();
        }

        public static bool HasTrail(object trail)
        {
            return HasTrail(trail as BreadCrumbTrail);
        }

        public BreadCrumbTrail Add(BreadCrumb breadCrumb)
        {
            BreadCrumbs.Add(breadCrumb);
            return this;
        }

        public BreadCrumbTrail Add(string name)
        {
            return Add(new BreadCrumb(name));
        }

        public BreadCrumbTrail Add(string name, string url)
        {
            return Add(new BreadCrumb(name, url));
        }
    }
}