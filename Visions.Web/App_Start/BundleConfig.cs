using System.Web.Optimization;

namespace Visions.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/creative").Include(
                        "~/Scripts/creative.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/scrollreveal").Include(
                        "~/Scirpts/scrollreveal.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/site-css").Include(
                      "~/Content/vendor/font-awesome/css/font-awesome.css",
                      "~/Content/vendor/bootstrap/css/bootstrap.css")
                      .Include("~/Content/css/creative.css", new CssRewriteUrlTransform()));
            
            bundles.Add(new StyleBundle("~/Content/site-less")
                .Include("~/Content/less/creative.less", new CssRewriteUrlTransform()));

            BundleTable.EnableOptimizations = true;
        }
    }
}
