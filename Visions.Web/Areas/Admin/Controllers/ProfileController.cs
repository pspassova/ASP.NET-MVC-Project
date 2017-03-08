using System.Web.Mvc;

namespace Visions.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        [HttpGet]
        public ActionResult Manage()
        {
            return View();
        }
    }
}