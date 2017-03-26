using Bytes2you.Validation;
using Visions.Models.Models;
using Visions.Web.Common.Contracts;
using Visions.Web.Models;

namespace Visions.Web.Common
{
    public class PhotoConverter : IPhotoConverter
    {
        public PhotoViewModel ConvertToViewModel(Photo photo)
        {
            Guard.WhenArgument(photo, "photo").IsNull().Throw();

            PhotoViewModel photoViewModel = new PhotoViewModel
            {
                Id = photo.Id,
                UserId = photo.UserId,
                Path = photo.Path,
                Likes = photo.Likes,
                Tags = photo.Tags,
                CreatedOn = photo.CreatedOn,
                IsDeleted = photo.IsDeleted
            };

            return photoViewModel;
        }
    }
}