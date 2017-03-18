using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.Mvc;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Web.Helpers.Contracts;

namespace Visions.Web.Areas.User.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUploadService<Photo> uploadPhotoService;
        private readonly IPhotoService photoService;
        private readonly IUploadPhoto photoUploader;
        private readonly IConvertTags tagsConverter;

        public ProfileController(
            IUploadService<Photo> uploadPhotoService,
            IPhotoService photoService,
            IUploadPhoto photoUploader,
            IConvertTags tagsConverter)
        {
            this.uploadPhotoService = uploadPhotoService;
            this.photoService = photoService;
            this.photoUploader = photoUploader;
            this.tagsConverter = tagsConverter;
        }

        [HttpGet]
        public ActionResult UserDashboard()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult UserDashboard(HttpPostedFileBase file, string tags)
        {
            this.UploadPhoto(file, tags);
            this.TempData["Success"] = "Upload successful";

            return this.RedirectToAction("UserDashboard");
        }

        [NonAction]
        public void UploadPhoto(HttpPostedFileBase file, string tags)
        {
            string userId = this.User.Identity.GetUserId();
            string physicalPath = Server.MapPath("~/Images/" + userId);
            string directory = this.photoUploader.GetDirectory(file, physicalPath);
            photoUploader.Upload(file, directory);

            string path = this.photoUploader.GetPathForDatabase(file, userId);
            Photo photo = this.photoService.Create(userId, path);
            this.uploadPhotoService.Upload(photo);
        }
    }
}