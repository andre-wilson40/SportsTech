using SportsTech.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportsTech.Web.Filters
{
    /// <summary>
    /// This class is used in conjunction with the HttpAjaxAttribute in order to provide a safe
    /// and convienent solution for enabling redirects when the action is called using Ajax.  It does
    /// this by hijacking the Result and returning a json object instead with the URL to be redirected to
    /// </summary>
    public class HttpAjaxRedirectFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var modelState = filterContext.Controller.ViewData.ModelState;

            if (!modelState.IsValid && IsPost(filterContext.RequestContext.HttpContext))
            {
                // If the model state is not valid for ajax requests then we don't even want to carry on with the action. In theory all of our forms
                // should be using clientside validation so it should all be caught there so any posts that make it through are via some other mechanism
                filterContext.HttpContext.Response.StatusCode = 400;

                filterContext.Result = NewtonSoftJson(new
                {
                    Message = "The data supplied was invalid",
                    Data = modelState
                });
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var redirectResult = filterContext.Result as RedirectToRouteResult;

            if (redirectResult != null && !filterContext.Canceled)
            {
                var values = redirectResult.RouteValues.Values.ToList();
                string actionName = values[0] as string;
                string controllerName = values[1] as string;

                UrlHelper urlHelper = new UrlHelper(filterContext.HttpContext.Request.RequestContext);
                filterContext.Result = JsonRedirectToAction(urlHelper, actionName, controllerName, redirectResult.RouteValues);
            }
        }

        protected bool IsPost(HttpContextBase httpContext)
        {
            return httpContext.Request.HttpMethod == "POST";
        }

        protected JsonResult NewtonSoftJson(object obj, JsonRequestBehavior behaviour = JsonRequestBehavior.AllowGet)
        {
            return new NetwonSoftJsonActionResult
            {
                Data = obj,
                JsonRequestBehavior = behaviour
            };
        }

        protected JsonResult JsonRedirectToAction(UrlHelper urlHelper, string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            return NewtonSoftJson(new
            {
                RedirectUrl = urlHelper.Action(actionName, controllerName, routeValues)
            });
        }
    }
}