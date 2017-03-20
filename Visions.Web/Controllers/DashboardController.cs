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
        public ActionResult Shared(int page, int pageSize)
        {
            IQueryable<PhotoViewModel> photos = this.photoService.GetAll()
                .Select(PhotoViewModel.FromPhoto)
                .OrderBy(photo => photo.CreatedOn);
            IPagedList<PhotoViewModel> photosPagedList = new PagedList<PhotoViewModel>(photos, page, pageSize);

            return this.View(photosPagedList);
        }

        [HttpGet]
        public ActionResult Sort(string text, int page, int pageSize)
        {
            IEnumerable<PhotoViewModel> photos = this.photoService.SortByTag(text)
                .AsQueryable()
                .Select(PhotoViewModel.FromPhoto);
            IPagedList<PhotoViewModel> photosPagedList = new PagedList<PhotoViewModel>(photos, page, pageSize);

            return this.View("Shared", photosPagedList);
        }
    }
}