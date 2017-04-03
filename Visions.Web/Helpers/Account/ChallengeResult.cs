using Bytes2you.Validation;
using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;

namespace Visions.Web.Helpers.Account
{
    public class ChallengeResult : HttpUnauthorizedResult
    {
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        public ChallengeResult(string provider, string redirectUri)
            : this(provider, redirectUri, null)
        {
        }

        public ChallengeResult(string provider, string redirectUri, string userId)
        {
            Guard.WhenArgument(provider, "provider").IsNull().Throw();
            Guard.WhenArgument(redirectUri, "redirectUri").IsNull().Throw();

            this.LoginProvider = provider;
            this.RedirectUri = redirectUri;
            this.UserId = userId;
        }

        public string LoginProvider
        {
            get; set;
        }

        public string RedirectUri
        {
            get; set;
        }

        public string UserId
        {
            get; set;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            AuthenticationProperties properties = new AuthenticationProperties { RedirectUri = RedirectUri };
            if (this.UserId != null)
            {
                properties.Dictionary[XsrfKey] = UserId;
            }

            context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
        }
    }
}