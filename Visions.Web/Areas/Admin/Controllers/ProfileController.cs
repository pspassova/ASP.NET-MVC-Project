using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Visions.Helpers.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Services.Enumerations;
using Visions.Web.Common.Contracts;
using Visions.Web.Models;

namespace Visions.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly HttpServerUtilityBase server;
        private readonly IUploadService<Tag> uploadTagService;
        private readonly IModifyService<Photo> modifyPhotoService;
        private readonly IDeleteService<Photo> deletePhotoService;
        private readonly IPhotoService photoService;
        private readonly IPhotoUploader photoUploader;
        private readonly IPhotoConverter photoConverter;
        private readonly ITagsHelper tagsHelper;

        public ProfileController(
            HttpServerUtilityBase server,
            IUploadService<Tag> uploadTagService,
            IModifyService<Photo> modifyPhotoService,
            IDeleteService<Photo> deletePhotoService,
            IPhotoService photoService,
            IPhotoUploader photoUploader,
            IPhotoConverter photoConverter,
            ITagsHelper tagsHelper)
        {
            Guard.WhenArgument(server, "server").IsNull().Throw();
            Guard.WhenArgument(uploadTagService, "uploadTagService").IsNull().Throw();
            Guard.WhenArgument(modifyPhotoService, "modifyPhotoService").IsNull().Throw();
            Guard.WhenArgument(deletePhotoService, "deletePhotoService").IsNull().Throw();
            Guard.WhenArgument(photoService, "photoService").IsNull().Throw();
            Guard.WhenArgument(photoUploader, "photoUploader").IsNull().Throw();
            Guard.WhenArgument(photoConverter, "photoConverter").IsNull().Throw();
            Guard.WhenArgument(tagsHelper, "tagsHelper").IsNull().Throw();

            this.server = server;
            this.uploadTagService = uploadTagService;
            this.modifyPhotoService = modifyPhotoService;
            this.deletePhotoService = deletePhotoService;
            this.photoService = photoService;
            this.photoUploader = photoUploader;
            this.photoConverter = photoConverter;
            this.tagsHelper = tagsHelper;
        }

        [HttpGet]
        public ActionResult Manage()
        {
            IEnumerable<PhotoViewModel> photos = this.photoService.GetAll(null, photo => photo.CreatedOn, OrderBy.Descending, PhotoViewModel.FromPhoto);

            return this.View(photos);
        }

        [HttpPost]
        public ActionResult Manage(HttpPostedFileBase file, string tags)
        {
            if (file == null)
            {
                this.TempData["Success"] = Resources.Constants.UploadFailedMessage;

                return this.RedirectToAction("Manage");
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

            PhotoViewModel photoViewModel = this.photoConverter.ConvertToViewModel(photo);
            return this.PartialView("_PhotoDetails", photoViewModel);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            Photo photo = this.photoService.GetById(id);
            if (photo == null)
            {
                return HttpNotFound();
            }

            PhotoViewModel photoViewModel = this.photoConverter.ConvertToViewModel(photo);
            return this.PartialView("_EditPhoto", photoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Path,CreatedOn")]PhotoViewModel photoViewModel)
        {
            Photo photo = this.photoService.GetById(photoViewModel.Id);
            if (photo == null)
            {
                this.TempData["Success"] = Resources.Constants.EditFailedMessage;

                return this.RedirectToAction("Edit");
            }

            photo.Id = photoViewModel.Id;
            photo.Path = photoViewModel.Path;
            photo.CreatedOn = photoViewModel.CreatedOn;

            this.modifyPhotoService.Edit(photo);
            this.TempData["Success"] = Resources.Constants.EditSuccessfulMessage;

            return this.PartialView("_EditPhoto", photoViewModel);
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            Photo photo = this.photoService.GetById(id);
            if (photo == null)
            {
                return HttpNotFound();
            }

            PhotoViewModel photoViewModel = this.photoConverter.ConvertToViewModel(photo);

            return this.PartialView("_DeletePhoto", photoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletingConfirmed(Guid id)
        {
            Photo photo = this.photoService.GetById(id);
            if (photo == null)
            {
                return HttpNotFound();
            }

            this.deletePhotoService.Delete(photo);

            return this.RedirectToAction("Manage");
        }
    }
}