using Bytes2you.Validation;
using System.Collections.Generic;
using System.Web;
using Visions.Auth.Contracts;
using Visions.Helpers.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Web.Common.Contracts;

namespace Visions.Web.Common
{
    public class PhotoUploader : IPhotoUploader
    {
        private readonly IPhotoHelper photoUploadHelper;
        private readonly IPhotoService photoService;
        private readonly IUploadService<Photo> uploadPhotoService;
        private readonly IUserProvider userProvider;

        public PhotoUploader(
             IPhotoHelper photoUploadHelper,
             IPhotoService photoService,
             IUploadService<Photo> uploadPhotoService,
             IUserProvider userProvider)
        {
            Guard.WhenArgument(photoUploadHelper, "photoUploadHelper").IsNull().Throw();
            Guard.WhenArgument(photoService, "photoService").IsNull().Throw();
            Guard.WhenArgument(uploadPhotoService, "uploadPhotoService").IsNull().Throw();
            Guard.WhenArgument(userProvider, "userProvider").IsNull().Throw();

            this.photoUploadHelper = photoUploadHelper;
            this.photoService = photoService;
            this.uploadPhotoService = uploadPhotoService;
            this.userProvider = userProvider;
        }

        public void UploadPhotos(HttpPostedFileBase file, string physicalPath, ICollection<Tag> tags)
        {
            string userId = this.userProvider.GetUserId();

            physicalPath += userId;
            this.photoUploadHelper.UploadToFileSystem(file, physicalPath);

            string path = this.photoUploadHelper.GetPathForDatabase();
            Photo photo = this.photoService.Create(userId, path, tags);
            this.uploadPhotoService.UploadToDatabase(photo);
        }
    }
}