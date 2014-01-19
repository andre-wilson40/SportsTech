using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SportsTech.Web.Filters
{
    /// <summary>
    ///  http://johan.driessen.se/posts/Updated-Anti-XSRF-Validation-for-ASP.NET-MVC-4-RC
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class WebValidateAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            var httpContext = filterContext.HttpContext;
            var cookie = httpContext.Request.Cookies[AntiForgeryConfig.CookieName];
            var formToken = GetFormToken(httpContext);

            AntiForgery.Validate(cookie != null ? cookie.Value : null, formToken);
        }

        private string GetFormToken(HttpContextBase httpContext)
        {
            string fieldName = AntiForgeryData.GetAntiForgeryTokenName();
            string token = string.Empty;

            if (httpContext.Request.IsAjaxRequest())
            {
                token = httpContext.Request.Headers[fieldName];
            }

            return string.IsNullOrEmpty(token) ?
                httpContext.Request.Form[fieldName] :
                token;
        }
    }

    internal sealed class AntiForgeryData
    {
        private const string AntiForgeryTokenFieldName = "__RequestVerificationToken";

        internal static string GetAntiForgeryTokenName()
        {
            return AntiForgeryTokenFieldName;
        }

    }
}