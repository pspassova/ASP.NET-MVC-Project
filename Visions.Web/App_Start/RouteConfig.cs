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
                "Default_paging",
                "{lang}/{controller}/{action}/{page}/{pageSize}/{text}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    page = 1,
                    pageSize = 4,
                    text = UrlParameter.Optional
                },
                constraints: new
                {
                    lang = "en|bg"
                }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index"
                }
            );
        }
    }
}
