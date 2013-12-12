using SportsTech.Web.Filters;
using System.Web;
using System.Web.Mvc;

namespace SportsTech.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new UnHandledExceptionAttribute());

            // Filter binding implemented using the NinJect MVC3 extension
            // http://www.planetgeek.ch/2010/11/13/official-ninject-mvc-extension-gets-support-for-mvc3/#more-2004
            filters.Add(new HttpAjaxRedirectFilter());

            //this.BindFilter<HttpAjaxRedirectFilter>(FilterScope.Action, 1).WhenActionMethodHas<HttpAjaxAttribute>();
        }
    }
}
