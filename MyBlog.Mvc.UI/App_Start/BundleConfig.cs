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
            bundles.Add(new StyleBundle("~/css/all").Include(
                "~/Content/bootstrap.min.css",
                "~/assets/assetcss/templatemo-stand-blog.css",
                "~/assets/assetcss/flex-slider.css",
                "~/assets/assetcss/owl.css",
                "~/assets/assetcss/social-buttons.css",
                "~/Content/css/main-layout.css"
                ));
            bundles.Add(new StyleBundle("~/css/custom").Include("~/assets/assetcss/custom.css"));


            bundles.Add(new StyleBundle("~/bundles/fontawesome").Include("~/Content/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/js/all").Include("~/Scripts/bootstrap.min.js", "~/Scripts/jquery-3.5.1.js"));

            bundles.Add(new ScriptBundle("~/temp/js/all").Include("~/assets/js/*.js"));


        }
    }
}