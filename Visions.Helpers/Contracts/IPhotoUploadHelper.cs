using System.Web;

namespace Visions.Helpers.Contracts
{
    public interface IPhotoUploadHelper
    {
        string GetPathForDatabase();

        void UploadToFileSystem(HttpPostedFileBase file, string physicalPath);
    }
}
