using System.Web.Mvc;
using System.Web.Routing;

namespace Visions.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.LowercaseUrls = true;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "DefaultPaging",
                "{controller}/{action}/{page}/{pageSize}",
                defaults: new
                {
                    controller = "Dashboard",
                    action = "Shared",
                    page = 1,
                    pageSize = 4
                }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}
