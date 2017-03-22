using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Visions.Helpers.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Services.Enumerations;
using Visions.Web.Helpers.Contracts;
using Visions.Web.Models;

namespace Visions.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUploadService<Tag> uploadTagService;
        private readonly IModifyService<Photo> modifyPhotoService;
        private readonly IDeleteService<Photo> deletePhotoService;
        private readonly IPhotoService photoService;
        private readonly IPhotoUploader photoUploader;
        private readonly ITagsHelper tagsConvertHelper;

        public ProfileController(
            IUploadService<Tag> uploadTagService,
            IModifyService<Photo> modifyPhotoService,
            IDeleteService<Photo> deletePhotoService,
            IPhotoService photoService,
            IPhotoUploader photoUploader,
            ITagsHelper tagsConvertHelper)
        {
            this.uploadTagService = uploadTagService;
            this.modifyPhotoService = modifyPhotoService;
            this.deletePhotoService = deletePhotoService;
            this.photoService = photoService;
            this.photoUploader = photoUploader;
            this.tagsConvertHelper = tagsConvertHelper;
        }

        [HttpGet]
        public ActionResult Manage()
        {
            IEnumerable<PhotoViewModel> photos = this.photoService.GetAll(null, photo => photo.CreatedOn, OrderBy.Descending, PhotoViewModel.FromPhoto);

            return View(photos);
        }

        [HttpPost]
        public ActionResult Manage(HttpPostedFileBase file, string tags)
        {
            if (file == null)
            {
                this.TempData["Success"] = "Upload failed";

                return this.RedirectToAction("Manage");
            }

            ICollection<Tag> convertedTags = this.tagsConvertHelper.CreateTags(tags);
            this.uploadTagService.UploadManyToDatabase(convertedTags);

            string userId = this.User.Identity.GetUserId();
            string physicalPath = Server.MapPath("~/Images/" + userId);

            this.photoUploader.UploadPhotos(userId, file, physicalPath, convertedTags);
            this.TempData["Success"] = "Upload successful";

            return this.RedirectToAction("Manage");
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            Photo photo = this.photoService.GetById(id);
            if (photo == null)
            {
                return HttpNotFound();
            }

            PhotoViewModel photoViewModel = PhotoViewModel.ConvertPhotoToViewModel(photo);
            return PartialView("_PhotoDetails", photoViewModel);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            Photo photo = this.photoService.GetById(id);
            if (photo == null)
            {
                return HttpNotFound();
            }

            PhotoViewModel photoViewModel = PhotoViewModel.ConvertPhotoToViewModel(photo);
            return PartialView("_EditPhoto", photoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Path,Tags,CreatedOn")]PhotoViewModel photoViewModel)
        {
            Photo photo = this.photoService.GetById(photoViewModel.Id);
            if (photo == null)
            {
                this.TempData["Success"] = "Edit failed";

                return this.RedirectToAction("Edit");
            }

            photo.Id = photoViewModel.Id;
            photo.Path = photoViewModel.Path;
            photo.Tags = photoViewModel.Tags;
            photo.CreatedOn = photoViewModel.CreatedOn;

            this.modifyPhotoService.Edit(photo);
            this.TempData["Success"] = "Edit successful";

            return this.RedirectToAction("Edit");
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {

            Photo photo = this.photoService.GetById(id);
            if (photo == null)
            {
                return HttpNotFound();
            }

            PhotoViewModel photoViewModel = PhotoViewModel.ConvertPhotoToViewModel(photo);
            return PartialView("_DeletePhoto", photoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Photo photo = this.photoService.GetById(id);
            this.deletePhotoService.Delete(photo);

            return RedirectToAction("Manage");
        }
    }
}