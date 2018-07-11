using System.Web;
using System.Web.Optimization;

namespace AnjaliMIS
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                  "~/assets/global/plugins/jquery.min.js",
                  "~/assets/global/plugins/jquery-migrate.min.js",
                  "~/assets/global/plugins/jquery-ui/jquery-ui.min.js",
                  "~/plugins/bootstrap/js/bootstrap.min.js",
                  "~/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                  "~/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                  "~/assets/global/plugins/jquery.blockui.min.js",
                  "~/assets/global/plugins/jquery.cokie.min.js",
                  "~/assets/global/plugins/uniform/jquery.uniform.min.js",
                  "~/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                  "~/assets/global/plugins/flot/jquery.flot.min.js",
                  "~/assets/global/plugins/flot/jquery.flot.resize.min.js",
                  "~/assets/global/plugins/flot/jquery.flot.categories.min.js",
                  "~/assets/global/plugins/jquery.pulsate.min.js",
                  "~/assets/global/plugins/bootstrap-daterangepicker/moment.min.js",
                  "~/assets/global/plugins/bootstrap-daterangepicker/daterangepicker.js",
                  "~/assets/global/plugins/fullcalendar/fullcalendar.min.js",
                  "~/assets/global/plugins/jquery-easypiechart/jquery.easypiechart.min.js",
                  "~/assets/global/plugins/jquery.sparkline.min.js",
                  "~/assets/global/scripts/metronic.js",
                  "~/assets/admin/layout/scripts/layout.js",
                  "~/assets/admin/layout/scripts/quick-sidebar.js",
                  "~/assets/admin/layout/scripts/demo.js",
                  "~/assets/admin/pages/scripts/index.js",
                  "~/assets/admin/pages/scripts/tasks.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/assets/global/plugins/font-awesome/css/font-awesome.min.css",
                       "~/assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
                       "~/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                       "~/assets/global/plugins/uniform/css/uniform.default.css",
                       "~/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                       "~/assets/global/css/components.css",
                       "~/assets/global/css/plugins.css",
                       "~/assets/admin/layout/css/layout.css",
                       "~/assets/admin/layout/css/themes/default.css",
                "~/assets/admin/layout/css/custom.css"));

            //bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
            //            "~/Content/themes/base/jquery.ui.core.css",
            //            "~/Content/themes/base/jquery.ui.resizable.css",
            //            "~/Content/themes/base/jquery.ui.selectable.css",
            //            "~/Content/themes/base/jquery.ui.accordion.css",
            //            "~/Content/themes/base/jquery.ui.autocomplete.css",
            //            "~/Content/themes/base/jquery.ui.button.css",
            //            "~/Content/themes/base/jquery.ui.dialog.css",
            //            "~/Content/themes/base/jquery.ui.slider.css",
            //            "~/Content/themes/base/jquery.ui.tabs.css",
            //            "~/Content/themes/base/jquery.ui.datepicker.css",
            //            "~/Content/themes/base/jquery.ui.progressbar.css",
            //            "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                       "~/assets/global/plugins/font-awesome/css/font-awesome.min.css",
                       "~/assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
                       "~/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                       "~/assets/global/plugins/uniform/css/uniform.default.css",
                       "~/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                       "~/assets/global/css/components.css",
                       "~/assets/global/css/plugins.css",
                       "~/assets/admin/layout/css/layout.css",
                       "~/assets/admin/layout/css/themes/default.css",
                       "~/assets/admin/layout/css/custom.css"));
        }
    }
}