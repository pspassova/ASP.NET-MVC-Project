using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using System.Web;
using Visions.Auth.Contracts;

namespace Visions.Auth
{
    public class UserProvider : IUserProvider
    {
        private readonly HttpContextBase context;

        public UserProvider(HttpContextBase context)
        {
            Guard.WhenArgument(context, "context").IsNull().Throw();

            this.context = context;
        }

        public string GetUserId()
        {
            return this.context.User.Identity.GetUserId();
        }

        public string GetUsername()
        {
            return this.context.User.Identity.Name;
        }
    }
}
