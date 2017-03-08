using System.Web.Mvc;

namespace Visions.Web.Areas.User.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        [HttpGet]
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}