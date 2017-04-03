using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Visions.Web.Models;
using Visions.Models.Models;
using Bytes2you.Validation;
using Visions.Auth.Contracts;

namespace Visions.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ISignInService signInService;
        private IUserService userService;

        public AccountController(ISignInService signInService, IUserService userService)
        {
            Guard.WhenArgument(signInService, "signInService").IsNull().Throw();
            Guard.WhenArgument(userService, "userService").IsNull().Throw();

            this.signInService = signInService;
            this.userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            this.ViewBag.ReturnUrl = returnUrl;

            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(loginViewModel);
            }

            SignInStatus status = await this.signInService.PasswordSignInAsync(
                loginViewModel.Email,
                loginViewModel.Password,
                loginViewModel.RememberMe,
                shouldLockout: false);

            switch (status)
            {
                case SignInStatus.Success:
                    return this.RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return this.View("Lockout");
                case SignInStatus.RequiresVerification:
                    return this.RedirectToAction("SendCode", new
                    {
                        ReturnUrl = returnUrl,
                        RememberMe = loginViewModel.RememberMe
                    });
                case SignInStatus.Failure:
                default:
                    this.ModelState.AddModelError("", "Invalid login attempt.");
                    return this.View(loginViewModel);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                User user = new User { UserName = model.Email, Email = model.Email };
                IdentityResult result = await this.userService.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await this.signInService.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    return this.RedirectToAction("Index", "Home");
                }

                this.AddErrors(result);
            }

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            this.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return this.RedirectToAction("Index", "Home");
        }

        #region Helpers
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return this.HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (this.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToAction("Index", "Home");
        }
        #endregion
    }
}