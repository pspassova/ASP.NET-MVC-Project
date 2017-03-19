using System.Web;

namespace Visions.Helpers.Contracts
{
    public interface IUploadPhoto
    {
        string GetDirectory(HttpPostedFileBase file, string physicalPath);

        string GetPathForDatabase(string directory);

        void Upload(HttpPostedFileBase file, string directory);
    }
}
