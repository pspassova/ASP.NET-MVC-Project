using System;
using System.IO;
using System.Web;
using Visions.Web.Helpers.Contracts;

namespace Visions.Web.Helpers
{
    public class UploadPhotoHelper : IUploadPhoto
    {

        public string GetDirectory(HttpPostedFileBase file, string physicalPath)
        {
            this.CreateDirectoryIfNotExist(physicalPath);

            string fileName = this.GetFileName(file);
            string directory = physicalPath + "/" + fileName;

            return directory;
        }

        public string GetPathForDatabase(HttpPostedFileBase file, string userId)
        {
            string fileName = this.GetFileName(file);
            string path = $"/Images/{userId}/{fileName}";

            return path;
        }

        public void Upload(HttpPostedFileBase file, string directory)
        {
            if (file != null && file.ContentLength > 0)
            {
                file.SaveAs(directory);
            }
        }

        private string GetFileName(HttpPostedFileBase file)
        {
            string guid = Guid.NewGuid().ToString();
            string fileName = guid + Path.GetFileName(file.FileName);

            return fileName;
        }

        private void CreateDirectoryIfNotExist(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}
