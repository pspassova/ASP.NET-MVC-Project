using System.Collections.Generic;
using System.Web;
using Visions.Models.Models;

namespace Visions.Web.Common.Contracts
{
    public interface IPhotoUploader
    {
        void UploadPhotos(HttpPostedFileBase file, string physicalPath, ICollection<Tag> tags);
    }
}
