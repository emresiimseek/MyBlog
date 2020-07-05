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
            bundles.Add(new StyleBundle("~/css/all").Include("~/Content/bootstrap.min.css"));
            bundles.Add(new ScriptBundle("~/js/all").Include("~/Scripts/bootstrap.min.js", "~/Scripts/jquery-3.5.1.min.js"));

        }
    }
}