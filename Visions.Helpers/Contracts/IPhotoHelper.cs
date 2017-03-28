using System.Web;

namespace Visions.Helpers.Contracts
{
    public interface IPhotoHelper
    {
        void UploadToFileSystem(HttpPostedFileBase file, string physicalPath);

        string GetPathForDatabase();
    }
}
