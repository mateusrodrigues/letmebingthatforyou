using System.Web.Mvc;
using System.Web.Routing;

namespace Lmbtfy.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Search",
                "",
                new { controller = "Home", action = "Index"}
            );

            routes.MapRoute(
                "OldSearch",
                "{predicate}",
                new { controller = "Home", action = "Index", predicate = "" }
            );

            routes.MapRoute(
                "Default",
                "{controller}/{action}",
                new { action = "Index" }
            );
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }
    }
}