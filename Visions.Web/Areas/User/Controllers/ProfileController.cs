using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Visions.Helpers.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Web.Models;

namespace Visions.Web.Areas.User.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUploadService<Photo> uploadPhotoService;
        private readonly IUploadService<Tag> uploadTagService;
        private readonly IPhotoService photoService;
        private readonly IUploadPhoto photoUploader;
        private readonly IConvertTags tagsConverter;

        public ProfileController(
            IUploadService<Photo> uploadPhotoService,
            IUploadService<Tag> uploadTagService,
            IPhotoService photoService,
            IUploadPhoto photoUploader,
            IConvertTags tagsConverter)
        {
            this.uploadPhotoService = uploadPhotoService;
            this.uploadTagService = uploadTagService;
            this.photoService = photoService;
            this.photoUploader = photoUploader;
            this.tagsConverter = tagsConverter;
        }

        [HttpGet]
        public ActionResult UserDashboard()
        {
            string userId = this.User.Identity.GetUserId();
            IQueryable<PhotoViewModel> photos = this.photoService.GetAllForUser(userId).Select(PhotoViewModel.FromPhoto);

            return this.View(photos);
        }

        [HttpPost]
        public ActionResult UserDashboard(HttpPostedFileBase file, string tags)
        {
            ICollection<Tag> convertedTags = this.tagsConverter.CreateTags(tags);
            this.uploadTagService.Upload(convertedTags);

            this.AddPhotos(file, convertedTags);
            this.TempData["Success"] = "Upload successful";

            return this.RedirectToAction("UserDashboard");
        }

        [NonAction]
        public void AddPhotos(HttpPostedFileBase file, ICollection<Tag> tags)
        {
            string userId = this.User.Identity.GetUserId();
            string physicalPath = Server.MapPath("~/Images/" + userId);
            string directory = this.photoUploader.GetDirectory(file, physicalPath);
            photoUploader.Upload(file, directory);

            string path = this.photoUploader.GetPathForDatabase(directory);
            Photo photo = this.photoService.Create(userId, path, tags);
            this.uploadPhotoService.Upload(photo);
        }
    }
}