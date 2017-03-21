using Microsoft.AspNet.Identity;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Visions.Helpers.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Services.Enumerations;
using Visions.Web.Helpers.Contracts;
using Visions.Web.Models;

namespace Visions.Web.Areas.User.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUploadService<Photo> uploadPhotoService;
        private readonly IUploadService<Tag> uploadTagService;
        private readonly IPhotoService photoService;
        private readonly IPhotoUploader photoUploader;
        private readonly ITagsHelper tagsConvertHelper;

        public ProfileController(
            IUploadService<Photo> uploadPhotoService,
            IUploadService<Tag> uploadTagService,
            IPhotoService photoService,
            IPhotoUploader photoUploader,
            ITagsHelper tagsConvertHelper)
        {
            this.uploadPhotoService = uploadPhotoService;
            this.uploadTagService = uploadTagService;
            this.photoService = photoService;
            this.photoUploader = photoUploader;
            this.tagsConvertHelper = tagsConvertHelper;
        }

        [HttpGet]
        public ActionResult UserDashboard(int page, int pageSize)
        {
            string userId = this.User.Identity.GetUserId();
            IEnumerable<PhotoViewModel> photos = this.photoService.GetAll(userId, photo => photo.CreatedOn, OrderBy.Descending, PhotoViewModel.FromPhoto);

            IPagedList<PhotoViewModel> photosPagedList = new PagedList<PhotoViewModel>(photos, page, pageSize);

            return this.View(photosPagedList);
        }

        [HttpPost]
        public ActionResult UserDashboard(HttpPostedFileBase file, string tags)
        {
            if (file == null)
            {
                this.TempData["Success"] = "Upload failed";

                return this.RedirectToAction("UserDashboard");
            }

            ICollection<Tag> convertedTags = this.tagsConvertHelper.CreateTags(tags);
            this.uploadTagService.UploadManyToDatabase(convertedTags);

            string userId = this.User.Identity.GetUserId();
            string physicalPath = Server.MapPath("~/Images/" + userId);

            this.photoUploader.UploadPhotos(userId, file, physicalPath, convertedTags);
            this.TempData["Success"] = "Upload successful";

            return this.RedirectToAction("UserDashboard");
        }

        [HttpGet]
        public ActionResult Sort(string text, int page, int pageSize)
        {
            this.ViewBag.SelectedTag = text;

            string userId = this.User.Identity.GetUserId();
            IEnumerable<PhotoViewModel> photos = this.photoService.SortByTag(text, userId)
                           .AsQueryable()
                           .Select(PhotoViewModel.FromPhoto);
            IPagedList<PhotoViewModel> photosPagedList = new PagedList<PhotoViewModel>(photos, page, pageSize);

            return this.View("UserDashboard", photosPagedList);
        }
    }
}