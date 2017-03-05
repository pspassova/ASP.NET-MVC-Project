using System.Web.Mvc;

namespace Visions.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Shared()
        {
            return View();
        }
    }
}