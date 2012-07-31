using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Lmbtfy.Web {
    public class MvcApplication : HttpApplication {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Search",
                "",
                new { controller = "Home", action = "Index" }
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

        protected void Application_Start() {
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}