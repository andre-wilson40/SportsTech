﻿using System.Web;
using System.Web.Optimization;

namespace SportsTech.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap.datatables.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-datetimepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/dataTable").Include(
                "~/Scripts/jquery.dataTables.js"));

            bundles.Add(new ScriptBundle("~/bundles/core").Include(
                "~/Scripts/site.*"));
            
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/themes/base/minified/juery-ui.min.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/bootstrap.datatables.css",
                      "~/Content/responsive.tables.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base").Include(
                      "~/Content/themes/base/minified/jquery-ui.min.css"));

            // http://qtip2.com
            bundles.Add(new ScriptBundle("~/bundles/qtip").Include(
                        "~/Scripts/qTip/jquery.qtip.js"));

            bundles.Add(new StyleBundle("~/Content/qTip").Include(
                "~/Scripts/qTip/jquery.qtip.css")
                );
        }
    }
}
