using Bytes2you.Validation;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Visions.Auth.Contracts;
using Visions.Helpers.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Services.Enumerations;
using Visions.Web.Common.Contracts;
using Visions.Web.Models;

namespace Visions.Web.Areas.User.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly HttpServerUtilityBase server;
        private readonly IUploadService<Photo> uploadPhotoService;
        private readonly IUploadService<Tag> uploadTagService;
        private readonly IPhotoService photoService;
        private readonly IPhotoUploader photoUploader;
        private readonly ITagsHelper tagsHelper;
        private readonly IUserProvider userProvider;
        private readonly IPagingProvider<PhotoViewModel> pagingProvider;

        public ProfileController(
            HttpServerUtilityBase server,
            IUploadService<Photo> uploadPhotoService,
            IUploadService<Tag> uploadTagService,
            IPhotoService photoService,
            IPhotoUploader photoUploader,
            ITagsHelper tagsHelper,
            IUserProvider userProvider,
            IPagingProvider<PhotoViewModel> pagingProvider)
        {
            Guard.WhenArgument(server, "server").IsNull().Throw();
            Guard.WhenArgument(uploadPhotoService, "uploadPhotoService").IsNull().Throw();
            Guard.WhenArgument(uploadTagService, "uploadTagService").IsNull().Throw();
            Guard.WhenArgument(photoService, "photoService").IsNull().Throw();
            Guard.WhenArgument(photoUploader, "photoUploader").IsNull().Throw();
            Guard.WhenArgument(tagsHelper, "tagsHelper").IsNull().Throw();
            Guard.WhenArgument(userProvider, "userProvider").IsNull().Throw();
            Guard.WhenArgument(pagingProvider, "pagingProvider").IsNull().Throw();

            this.server = server;
            this.uploadPhotoService = uploadPhotoService;
            this.uploadTagService = uploadTagService;
            this.photoService = photoService;
            this.photoUploader = photoUploader;
            this.tagsHelper = tagsHelper;
            this.userProvider = userProvider;
            this.pagingProvider = pagingProvider;
        }

        [HttpGet]
        public ActionResult Dashboard(int page, int pageSize)
        {
            string userId = this.userProvider.GetUserId();
            IEnumerable<PhotoViewModel> photos = this.photoService.GetAllOrderedByCreatedOn(OrderBy.Descending, userId)
                .Select(PhotoViewModel.FromPhoto);

            IPagedList<PhotoViewModel> photosPagedList = this.pagingProvider.CreatePagedList(photos, page, pageSize);

            return this.View(photosPagedList);
        }

        [HttpPost]
        public ActionResult Dashboard(HttpPostedFileBase file, string tags)
        {
            if (file == null)
            {
                this.TempData["Success"] = Resources.Constants.UploadFailedMessage;

                return this.RedirectToAction("Dashboard");
            }

            ICollection<Tag> convertedTags = new List<Tag>();
            if (tags != null)
            {
                convertedTags = this.tagsHelper.CreateTags(tags);
                this.uploadTagService.UploadManyToDatabase(convertedTags);
            }

            string physicalPath = this.server.MapPath("~/Images/");
            this.photoUploader.UploadPhotos(file, physicalPath, convertedTags);
            this.TempData["Success"] = Resources.Constants.UploadSuccessfulMessage;

            return this.RedirectToAction("Dashboard");
        }

        [HttpGet]
        public ActionResult Sort(string text, int page, int pageSize)
        {
            this.ViewBag.SelectedTag = text;

            string userId = this.userProvider.GetUserId();
            IEnumerable<PhotoViewModel> photos = this.photoService.SortByTag(text, userId)
                           .AsQueryable()
                           .Select(PhotoViewModel.FromPhoto);
            IPagedList<PhotoViewModel> photosPagedList = this.pagingProvider.CreatePagedList(photos, page, pageSize);

            return this.View("Dashboard", photosPagedList);
        }
    }
}