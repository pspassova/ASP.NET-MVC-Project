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
                "Admin_default",
                "{lang}/admin/{controller}/{action}/{page}/{pageSize}/{text}",
                defaults: new
                {
                    lang = "en",
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
        }
    }
}