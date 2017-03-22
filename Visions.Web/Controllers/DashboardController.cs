using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Visions.Services.Contracts;
using Visions.Web.Models;
using PagedList;

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
        public ActionResult Shared(string text, int page, int pageSize)
        {
            this.ViewBag.SelectedTag = text;

            IEnumerable<PhotoViewModel> photos = this.photoService.SortByTag(text)
                .AsQueryable()
                .Select(PhotoViewModel.FromPhoto);
            IPagedList<PhotoViewModel> photosPagedList = new PagedList<PhotoViewModel>(photos, page, pageSize);

            return this.View(photosPagedList);
        }

        [HttpPost]
        public ActionResult Search(string query, int page, int pageSize)
        {
            IEnumerable<PhotoViewModel> photos = this.photoService.SortByTag(query)
                .AsQueryable()
                .Select(PhotoViewModel.FromPhoto);
            IPagedList<PhotoViewModel> photosPagedList = new PagedList<PhotoViewModel>(photos, page, pageSize);

            return this.View("Shared", photosPagedList);
        }
    }
}