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
using Visions.Web.Models;

namespace Visions.Web.Areas.User.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUploadService<Photo> uploadPhotoService;
        private readonly IUploadService<Tag> uploadTagService;
        private readonly IPhotoService photoService;
        private readonly IPhotoUploadHelper photoUploadHelper;
        private readonly ITagsConvertHelper tagsConvertHelper;

        public ProfileController(
            IUploadService<Photo> uploadPhotoService,
            IUploadService<Tag> uploadTagService,
            IPhotoService photoService,
            IPhotoUploadHelper photoUploadHelper,
            ITagsConvertHelper tagsConvertHelper)
        {
            this.uploadPhotoService = uploadPhotoService;
            this.uploadTagService = uploadTagService;
            this.photoService = photoService;
            this.photoUploadHelper = photoUploadHelper;
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
            ICollection<Tag> convertedTags = this.tagsConvertHelper.CreateTags(tags);
            this.uploadTagService.UploadManyToDatabase(convertedTags);

            this.UploadPhotos(file, convertedTags);
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

        [NonAction]
        public void UploadPhotos(HttpPostedFileBase file, ICollection<Tag> tags)
        {
            string userId = this.User.Identity.GetUserId();
            string physicalPath = Server.MapPath("~/Images/" + userId);
            this.photoUploadHelper.UploadToFileSystem(file, physicalPath);

            string path = this.photoUploadHelper.GetPathForDatabase();
            Photo photo = this.photoService.Create(userId, path, tags);
            this.uploadPhotoService.UploadToDatabase(photo);
        }
    }
}