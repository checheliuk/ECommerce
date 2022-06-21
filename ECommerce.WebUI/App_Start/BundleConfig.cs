using System.Web.Optimization;

namespace ECommerce.WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                      "~/Scripts/modernizr-*",
                      "~/Scripts/jquery-{version}.js",
                      "~/Scripts/popper.min.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                     "~/Scripts/modernizr-*",
                     "~/Scripts/jquery-{version}.js",
                     "~/Scripts/respond.js",
                     "~/Scripts/layout.js",
                     "~/Scripts/scripts-customer.js",
                     "~/Scripts/cart.js",
                     "~/Scripts/analytics.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/vendor.css",
                      "~/Content/site.css",
                      "~/Content/main.css"));

            bundles.Add(new StyleBundle("~/Content/Admin/css").Include(
                     "~/Content/bootstrap.min.css",
                     "~/Content/admin.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
