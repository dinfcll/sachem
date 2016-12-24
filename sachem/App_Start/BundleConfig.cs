using System.Web.Optimization;

namespace sachem
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-1.10.2.js",
                "~/Scripts/jquery.unobtrusive-ajax.js",
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/tinymce").Include(
                "~/Scripts/tinymce/tinymce.js",
                "~/Scripts/tinymce/jquery.tinymce.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css",
                "~/Content/pagedlist.css"));

            bundles.Add(new ScriptBundle("~/bundles/dropzonescripts").Include(
                "~/Scripts/dropzone/dropzone.js"));

            bundles.Add(new StyleBundle("~/Content/dropzonescss").Include(
                "~/Scripts/dropzone/basic.css",
                "~/Scripts/dropzone/dropzone.css"));
        }
    }
}
