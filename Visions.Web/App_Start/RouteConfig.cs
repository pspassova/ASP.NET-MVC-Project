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

            //routes.MapRoute(
            //    name: "Lang",
            //    url: "{lang}/{controller}/{action}",
            //    defaults: new
            //    {
            //        lang = UrlParameter.Optional,
            //        controller = "Home",
            //        action = "Index"
            //    }
            //);
            routes.MapRoute(
                "Default_paging",
                "{lang}/{controller}/{action}/{page}/{pageSize}/{text}",
                defaults: new
                {
                    lang = UrlParameter.Optional,
                    controller = "Home",
                    action = "Index",
                    page = 1,
                    pageSize = 4,
                    text = UrlParameter.Optional
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
