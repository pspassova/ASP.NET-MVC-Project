using System.Web.Mvc;

namespace Visions.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;

            context.MapRoute(
                "Admin_default_paging",
                "{lang}/admin/{controller}/{action}/{page}/{pageSize}",
                defaults: new
                {
                    lang = "en",
                    controller = "Home",
                    action = "Index",
                    page = 1,
                    pageSize = 4
                }
            );
            context.MapRoute(
                "Admin_default",
                "{lang}/admin/{controller}/{action}",
                defaults: new
                {
                    lang = "en",
                    action = "Index"
                }
            );
        }
    }
}