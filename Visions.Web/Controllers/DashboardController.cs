using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Visions.Services.Contracts;
using Visions.Web.Models;

namespace Visions.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IPhotoService photoService;

        public DashboardController(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        [HttpGet]
        public ActionResult Shared()
        {
            IQueryable<PhotoViewModel> photos = this.photoService.GetAll().Select(PhotoViewModel.FromPhoto);

            return this.View(photos);
        }

        [HttpGet]
        public ActionResult Sort(string text)
        {
            IEnumerable<PhotoViewModel> photos = this.photoService.SortByTag(text).AsQueryable().Select(PhotoViewModel.FromPhoto);

            return this.View("Shared", photos);
        }
    }
}