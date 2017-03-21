using System.Web;

namespace Visions.Helpers.Contracts
{
    public interface IPhotoHelper
    {
        string GetPathForDatabase();

        void UploadToFileSystem(HttpPostedFileBase file, string physicalPath);
    }
}
