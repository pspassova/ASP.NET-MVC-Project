using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Visions.Services.Contracts;
using Visions.Web.Models;
using PagedList;
using Bytes2you.Validation;
using Visions.Web.Common.Contracts;

namespace Visions.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IPhotoService photoService;
        private readonly IPagingProvider<PhotoViewModel> pagingProvider;

        public DashboardController(IPhotoService photoService, IPagingProvider<PhotoViewModel> pagingProvider)
        {
            Guard.WhenArgument(photoService, "photoService").IsNull().Throw();
            Guard.WhenArgument(pagingProvider, "pagingProvider").IsNull().Throw();

            this.photoService = photoService;
            this.pagingProvider = pagingProvider;
        }

        [HttpGet]
        public ActionResult Shared(string text, int page, int pageSize)
        {
            this.ViewBag.SelectedTag = text;

            IEnumerable<PhotoViewModel> photos = this.photoService.SortByTag(text)
                .AsQueryable()
                .Select(PhotoViewModel.FromPhoto);
            IPagedList<PhotoViewModel> photosPagedList = this.pagingProvider.CreatePagedList(photos, page, pageSize);

            return this.View(photosPagedList);
        }

        [HttpPost]
        public ActionResult Search(string query, int page, int pageSize)
        {
            IEnumerable<PhotoViewModel> photos = this.photoService.SortByTag(query)
                .AsQueryable()
                .Select(PhotoViewModel.FromPhoto);
            IPagedList<PhotoViewModel> photosPagedList = this.pagingProvider.CreatePagedList(photos, page, pageSize);

            return this.View("Shared", photosPagedList);
        }
    }
}