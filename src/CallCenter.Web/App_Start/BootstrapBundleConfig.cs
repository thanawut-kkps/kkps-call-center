using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace BootstrapSupport
{
    public class BootstrapBundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-migrate-{version}.js",                
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js", 
                "~/Scripts/jquery-ui-{version}.js"
                ));

            bundles.Add(new ScriptBundle("~/bootstap").Include(
                "~/Scripts/moment.js",    
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootstrap-datepicker.js",                  
                "~/Scripts/bootstrap-datetimepicker.js",
                "~/Scripts/jquery.validate.unobtrusive-custom-for-bootstrap.js",
                "~/Scripts/jquery.validate.unobtrusive.bootstrap.js"                
                ));

            bundles.Add(new ScriptBundle("~/jsplugin").Include(
                //"~/Scripts/jquery.dataTables.js",
                //"~/Scripts/dataTables.bootstrap.js"
                "~/Scripts/bootstrap-sortable.js"
                ));


            bundles.Add(new ScriptBundle("~/bootstrap-notify").Include(
                "~/Scripts/bootstrap-notify-3.1.3/bootstrap-notify.js",
                "~/Scripts/bootstrap-notify-3.1.3/bootstrap-notify-custom-default.js"
                ));

            bundles.Add(new ScriptBundle("~/jscommon").Include(
                "~/Scripts/Common/jquery.validation.reset.js",
                "~/Scripts/Common/Encoder.js",
                "~/Scripts/Common/Dialog.js",
                "~/Scripts/Common/Form.js",
                "~/Scripts/Common/jsonSerializeObject.js",
                "~/Scripts/Common/SelectionDialog.js",
                "~/Scripts/Common/AccountPortfolioDialog.js"                             
            ));

            bundles.Add(new ScriptBundle("~/notypackaged").Include(
                "~/Scripts/noty/packaged/jquery.noty.packaged.js"
            ));

            bundles.Add(new ScriptBundle("~/signalr").Include(
                "~/Scripts/jquery.signalR-{version}.js",
                "~/Scripts/signalR/jquery.signalR.custom.notification.js"
            ));

            bundles.Add(new StyleBundle("~/content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/body.css",
                "~/Content/bootstrap-responsive.css",
                "~/Content/bootstrap-mvc-validation.css",
                "~/Content/bootstrap-datepicker.css",    
                "~/Content/bootstrap-datetimepicker.css",                
                "~/Content/font-awesome.css",
                "~/Content/paging.css",
                //"~/Content/jquery.dataTables.css",
                //"~/Content/dataTables.bootstrap.css",    
                "~/Content/bootstrap-sortable.css",
                "~/Content/animate.css",
                "~/Content/Site.css"
                ));
        }
    }
}