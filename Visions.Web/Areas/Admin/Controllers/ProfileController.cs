using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Visions.Helpers.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Services.Enumerations;
using Visions.Web.Models;

namespace Visions.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUploadService<Photo> uploadPhotoService;
        private readonly IUploadService<Tag> uploadTagService;
        private readonly IModifyService<Photo> modifyPhotoService;
        private readonly IDeleteService<Photo> deletePhotoService;
        private readonly IPhotoService photoService;
        private readonly IPhotoUploadHelper photoUploadHelper;
        private readonly ITagsConvertHelper tagsConvertHelper;

        public ProfileController(
            IUploadService<Photo> uploadPhotoService,
            IUploadService<Tag> uploadTagService,
             IModifyService<Photo> modifyPhotoService,
             IDeleteService<Photo> deletePhotoService,
            IPhotoService photoService,
            IPhotoUploadHelper photoUploadHelper,
            ITagsConvertHelper tagsConvertHelper)
        {
            this.uploadPhotoService = uploadPhotoService;
            this.uploadTagService = uploadTagService;
            this.modifyPhotoService = modifyPhotoService;
            this.deletePhotoService = deletePhotoService;
            this.photoService = photoService;
            this.photoUploadHelper = photoUploadHelper;
            this.tagsConvertHelper = tagsConvertHelper;
        }

        [HttpGet]
        public ActionResult Manage()
        {
            IEnumerable<PhotoViewModel> photos = this.photoService.GetAll(null, photo => photo.CreatedOn, OrderBy.Descending, PhotoViewModel.FromPhoto);

            return View(photos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(HttpPostedFileBase file, string tags)
        {
            ICollection<Tag> convertedTags = this.tagsConvertHelper.CreateTags(tags);
            this.uploadTagService.UploadManyToDatabase(convertedTags);

            this.UploadPhotos(file, convertedTags);
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
        public ActionResult Edit([Bind (Include ="Id")] PhotoViewModel photoViewModel)
        {
            Photo photo = this.photoService.GetById(photoViewModel.Id);
            if (photo == null)
            {
                return HttpNotFound();
            }

            this.modifyPhotoService.Edit(photo);

            return RedirectToAction("Manage");
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

        // TODO: take this to another class
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