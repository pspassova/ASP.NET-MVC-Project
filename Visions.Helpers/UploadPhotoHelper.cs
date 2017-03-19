using System;
using System.IO;
using System.Web;
using Visions.Helpers.Contracts;

namespace Visions.Helpers
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

        public string GetPathForDatabase(string directory)
        {
            string textToCrop = "Images\\";
            string path = "/Images/" + directory.Substring(directory.IndexOf(textToCrop) + textToCrop.Length);

            return path;
        }

        private string GetFileName(HttpPostedFileBase file)
        {
            string guid = Guid.NewGuid().ToString();
            string fileName = guid + Path.GetFileName(file.FileName);

            return fileName;
        }

        public void Upload(HttpPostedFileBase file, string directory)
        {
            if (file != null && file.ContentLength > 0)
            {
                file.SaveAs(directory);
            }
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
