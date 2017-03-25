using Microsoft.AspNet.Identity;
using System.Web;
using Visions.Auth.Contracts;

namespace Visions.Auth
{
    public class UserProvider : IUserProvider
    {
        public string GetUserId()
        {
            return HttpContext.Current.User.Identity.GetUserId();
        }

        public string GetUsername()
        {
            return HttpContext.Current.User.Identity.GetUserName();
        }
    }
}
