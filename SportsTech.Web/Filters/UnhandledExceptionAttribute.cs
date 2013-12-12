using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SportsTech.Web.Filters
{
    /// <summary>
    /// Unhandled exception code supplied by svn 19-June as the way in which Intergen handles exceptions when supplied via ajax
    /// </summary>
    public class UnHandledExceptionAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext != null)
            {
                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    // all errors except timeout are InternalServerErrors
                    int errorCode = (int)HttpStatusCode.InternalServerError;

                    if (filterContext.Exception is HttpException)
                    {
                        // get the exception
                        HttpException ex = filterContext.Exception as HttpException;

                        // 408 error code indicates session timeout thrown by api controllers
                        if (ex.GetHttpCode() == 408)
                        {
                            // timeout has occcurred so need to pass 408 up to ui for handling
                            errorCode = 408;
                        }
                    }
                    System.Diagnostics.Trace.WriteLine(filterContext.Exception.ToString());

                    // log the error here.
                    filterContext.ExceptionHandled = true;
                    filterContext.HttpContext.Response.StatusCode = errorCode;
                    filterContext.HttpContext.Response.StatusDescription = filterContext.Exception.Message;
                }
                else
                {
                    // do nothing as is not ajax
                    base.OnException(filterContext);
                }
            }
            // ready
        }
    }
}