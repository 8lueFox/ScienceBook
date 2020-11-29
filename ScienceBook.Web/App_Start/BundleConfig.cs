using System.Web;
using System.Web.Optimization;

namespace ScienceBook.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        { 
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/sidebar.css",
                      "~/Content/main.css"));

            bundles.Add(new StyleBundle("~/Content/loginCSS").Include(
                      "~/Content/login_css.css"));
        }
    }
}
