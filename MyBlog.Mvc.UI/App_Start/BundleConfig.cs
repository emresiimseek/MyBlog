using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MyBlog.Mvc.UI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterRoutes(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css/all").Include("~/Content/css/main-layout.css", "~/Content/bootstrap.min.css", "~/Content/css/font-awesome.min.css"));
            bundles.Add(new ScriptBundle("~/js/all").Include("~/Scripts/bootstrap.min.js", "~/Scripts/jquery-3.5.1.min.js"));

            bundles.Add(new StyleBundle("~/temp/css").Include("~/assets/css/*.css"));
            bundles.Add(new ScriptBundle("~/temp/js").Include("~/assets/js/*.js"));


        }
    }
}