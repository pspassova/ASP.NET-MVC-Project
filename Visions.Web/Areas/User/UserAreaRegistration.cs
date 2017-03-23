using System.Web.Mvc;

namespace Visions.Web.Areas.User
{
    public class UserAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "User";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;

            context.MapRoute(
                "User_default_paging",
                "{lang}/user/{controller}/{action}/{page}/{pageSize}/{text}",
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
            context.MapRoute(
                "User_default",
                "{lang}/user/{controller}/{action}/{id}",
                defaults: new
                {
                    lang = UrlParameter.Optional,
                    id = UrlParameter.Optional
                }
            );
        }
    }
}