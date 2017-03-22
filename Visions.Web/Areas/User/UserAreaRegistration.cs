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
                "UserDefaultPaging",
                "user/{controller}/{action}/{page}/{pageSize}",
                defaults: new
                {
                    page = 1,
                    pageSize = 4
                }
            );
            context.MapRoute(
                "User_default",
                "user/{controller}/{action}/{id}",
                defaults: new
                {
                    id = UrlParameter.Optional
                }
            );
        }
    }
}