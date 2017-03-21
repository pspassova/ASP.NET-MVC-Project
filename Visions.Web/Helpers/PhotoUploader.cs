using System.Collections.Generic;
using System.Web;
using Visions.Helpers.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Web.Helpers.Contracts;

namespace Visions.Web.Helpers
{
    public class PhotoUploader : IPhotoUploader
    {
        private readonly IPhotoHelper photoUploadHelper;
        private readonly IPhotoService photoService;
        private readonly IUploadService<Photo> uploadPhotoService;

        public PhotoUploader(
             IPhotoHelper photoUploadHelper,
             IPhotoService photoService,
             IUploadService<Photo> uploadPhotoService)
        {
            this.photoUploadHelper = photoUploadHelper;
            this.photoService = photoService;
            this.uploadPhotoService = uploadPhotoService;
        }

        public void UploadPhotos(string userId, HttpPostedFileBase file, string physicalPath, ICollection<Tag> tags)
        {
            this.photoUploadHelper.UploadToFileSystem(file, physicalPath);

            string path = this.photoUploadHelper.GetPathForDatabase();
            Photo photo = this.photoService.Create(userId, path, tags);
            this.uploadPhotoService.UploadToDatabase(photo);
        }
    }
}