using Microsoft.AspNet.Identity;
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
            string userId = this.User.Identity.GetUserId();
            IQueryable<PhotoViewModel> photos = this.photoService.GetAllForUser(userId).Select(PhotoViewModel.FromPhoto);

            return this.View(photos);
        }

        [HttpPost]
        public ActionResult UserDashboard(HttpPostedFileBase file)
        {
            this.UploadPhoto(file);
            this.TempData["Success"] = "Upload successful";

            return this.RedirectToAction("UserDashboard");
        }

        [NonAction]
        public void UploadPhoto(HttpPostedFileBase file)
        {
            string userId = this.User.Identity.GetUserId();
            string physicalPath = Server.MapPath("~/Images/" + userId);
            string directory = this.photoUploader.GetDirectory(file, physicalPath);
            photoUploader.Upload(file, directory);


            string textToCrop = "Images\\";
            string path = "/Images/" + directory.Substring(directory.IndexOf(textToCrop) + textToCrop.Length);
            Photo photo = this.photoService.Create(userId, path);
            this.uploadPhotoService.Upload(photo);
        }
    }
}