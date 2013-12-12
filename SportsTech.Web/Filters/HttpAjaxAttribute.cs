using SportsTech.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsTech.Web.Filters
{
    /// <summary>
    /// Represents an attribute that is used to restrict an action method so that the method handles only HTTP AJAX requests.
    /// 
    /// Code based on: http://helios.ca/2009/05/27/aspnet-mvc-action-filter-ajax-only-attribute/
    /// </summary>
    public class HttpAjaxAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, System.Reflection.MethodInfo methodInfo)
        {
            if (controllerContext.HttpContext.Request.IsAjaxRequest())
                return true;
            else
                throw new ResourceNotFoundException();
        }
    }
}