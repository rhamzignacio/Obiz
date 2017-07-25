using System.Web;
using System.Web.Optimization;

namespace Obiz
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/content/css").Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/bootstrap-responsive.min.css",
                "~/Content/css/fullcalendar.css",
                "~/Content/css/matrix-style.css",
                "~/Content/css/matrix-media.css",
                "~/Content/font-awesome/css/font-awesome.css",
                "~/Content/css/jquery.gritter.cs",
                "~/Content/css/angular-growl.min.css",
              //  "~/Content/css/uniform.css",
                "~/Content/css/bootstrap-wysihtml5.css",
                "~/Content/select2/dist/css/select2.min.css",
                "~/Content/css/jquery.dataTables.css",
                "~/Content/css/dataTables.bootstrap.css"
                ));

            bundles.Add(new StyleBundle("~/content/login").Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/bootstrap-responsive.min.css",
                "~/Content/css/matrix-login.css",
                "~/Content/font-awesome/css/font-awesome.css",
                "~/Content/css/angular-growl.min.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Content/js/jquery.min.js",
                "~/Content/angular-1.6.0/angular.min.js",
                "~/Content/js/bootstrap-datepicker.js",
                "~/Content/js/bootstrap.min.js",
                "~/Content/js/excanvas.min.js",
                "~/Content/js/fullcalendar.min.js",
                "~/Content/js/masked.js",
                "~/Content/js/matrix.js",
               "~/Content/select2/dist/js/select2.full.js",
                "~/Content/js/wysihtml5-0.3.0.js",
                "~/Content/js/angular-growl.min.js",
                "~/Content/js/wysihtml5-0.3.0.js",
               // "~/Content/js/jquery.uniform.js",
                "~/Content/js/jquery.dataTables.js",
                "~/Content/angular-file-upload.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/App/App.js",
                "~/App/Controller/Login.js",
                "~/App/Controller/SalesReport.js",
                "~/App/Controller/Admin.js",
                "~/App/Controller/ServiceIssueLogs.js",
                "~/App/Controller/AEFUR.js"
                ));

            bundles.Add(new ScriptBundle("~/content/chart").Include(
                "~/Scripts/moment.js",
                "~/Content/chart/charts/Chart.Bar.js",
                "~/Content/chart/charts/Chart.Bubble.js",
                "~/Content/chart/charts/Chart.Doughnut.js",
                "~/Content/chart/charts/Chart.Line.js",
                "~/Content/chart/charts/Chart.PolarArea.js",
                "~/Content/chart/charts/Chart.Radar.js",
                "~/Content/chart/charts/Chart.Scatter.js",

                "~/Content/chart/controllers/controller.bar.js",
                "~/Content/chart/controllers/controller.bubble.js",
                "~/Content/chart/controllers/controller.doughtnut.js",
                "~/Content/chart/controllers/controller.line.js",
                "~/Content/chart/controllers/controller.polarArea.js",
                "~/Content/chart/controllers/controller.radar.js",

                "~/Content/chart/core/core.js",
                "~/Content/chart/core/core.animation.js",
                "~/Content/chart/core/core.canvasHelpers.js",
                "~/Content/chart/core/core.element.js",
                "~/Content/chart/core/core.plugin.js",
                "~/Content/chart/core/core.controller.js",
                "~/Content/chart/core/core.datasetController.js",
                "~/Content/chart/core/core.layoutService.js",
                "~/Content/chart/core/core.scaleService.js",
                "~/Content/chart/core/core.ticks.js",
                "~/Content/chart/core/core.scale.js",
                "~/Content/chart/core/core.title.js",
                "~/Content/chart/core/core.legend.js",
                "~/Content/chart/core/core.interaction.js",
                "~/Content/chart/core/core.tooltip.js",

                "~/Content/chart/elements/element.arc.js",
                "~/Content/chart/elements/element.line.js",
                "~/Content/chart/elements/element.point.js",
                "~/Content/chart/elements/element.rectangle.js",

                "~/Content/chart/scales/scale.category.js",
                "~/Content/chart/scales/scale.linear.js",
                "~/Content/chart/scales/scale.linearbase.js",
                "~/Content/chart/scales/scale.logarithmic",
                "~/Content/chart/scales/scale.radialLiner.js",
                "~/Content/chart/scales/scale.time.js"

                //"~/Scripts/Chart.min.js"
                ));
        }
    }
}
