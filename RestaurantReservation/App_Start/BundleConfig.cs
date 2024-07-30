using System.Web;
using System.Web.Optimization;

namespace RestaurantReservation
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

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/login").Include(
            "~/Content/js/frontend/login.js"));

            bundles.Add(new ScriptBundle("~/bundles/restaurant").Include(
            "~/Content/js/frontend/restaurant.js"));

            bundles.Add(new ScriptBundle("~/bundles/users").Include(
            "~/Content/js/frontend/users.js"));

            bundles.Add(new ScriptBundle("~/bundles/reservation").Include(
           "~/Content/js/frontend/reservation.js"));

            bundles.Add(new ScriptBundle("~/bundles/clientsignup").Include(
           "~/Content/js/frontend/clientsignup.js"));

            bundles.Add(new ScriptBundle("~/bundles/clientlogin").Include(
           "~/Content/js/frontend/clientlogin.js"));

            bundles.Add(new ScriptBundle("~/bundles/clientreservation").Include(
           "~/Content/js/frontend/clientreservation.js"));

            bundles.Add(new ScriptBundle("~/bundles/clientbooking").Include(
           "~/Content/js/frontend/clientbooking.js"));
        }
    }
}
