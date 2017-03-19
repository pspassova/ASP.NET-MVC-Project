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
                "User_default",
                "User/{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Profile",
                    action = "UserDashboard",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}