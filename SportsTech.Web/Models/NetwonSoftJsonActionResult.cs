using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsTech.Web.Models
{
    public class NetwonSoftJsonActionResult : System.Web.Mvc.JsonResult
    {
        public NetwonSoftJsonActionResult()
        {
            JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        }

        public NetwonSoftJsonActionResult(object data)
            : this()
        {
            Data = data;
        }

        public override void ExecuteResult(System.Web.Mvc.ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                String.Equals(context.HttpContext.Request.HttpMethod,
                    "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("MvcResources.JsonRequest_GetNotAllowed");
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (!String.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data != null)
            {
                var serializer = new Newtonsoft.Json.JsonSerializer();
                var txr = new Newtonsoft.Json.JsonTextWriter(context.HttpContext.Response.Output);
                serializer.Serialize(txr, this.Data);
            }
        }
    }
}