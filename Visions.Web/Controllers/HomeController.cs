using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Visions.Services.Contracts;
using Visions.Web.Models;

namespace Visions.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPhotoService photoService;

        public HomeController(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Shared()
        {
            IEnumerable<PhotoViewModel> photos = this.photoService.GetAll()
                 .AsQueryable()
                 .Select(PhotoViewModel.FromPhoto);

            return View(photos);
        }
    }
}