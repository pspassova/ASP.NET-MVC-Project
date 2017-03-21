using System.Collections.Generic;
using System.Web;
using Visions.Models.Models;

namespace Visions.Web.Helpers.Contracts
{
    public interface IPhotoUploader
    {
        void UploadPhotos(string userId, HttpPostedFileBase file, string physicalPath, ICollection<Tag> tags);
    }
}
