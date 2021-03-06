﻿using System;
using System.IO;
using System.Web;
using Visions.Helpers.Contracts;

namespace Visions.Helpers
{
    public class PhotoHelper : IPhotoHelper
    {
        private static string directory;

        private const string UploadedImagesFolderName = "Images";

        public void UploadToFileSystem(HttpPostedFileBase file, string physicalPath)
        {
            directory = this.GetDirectory(file, physicalPath);

            if (file != null)
            {
                file.SaveAs(directory);
            }
        }

        public string GetPathForDatabase()
        {
            string textToCrop = $"{UploadedImagesFolderName}\\";
            string path = $"/{UploadedImagesFolderName}/" + directory.Substring(directory.IndexOf(textToCrop) + textToCrop.Length);

            return path;
        }

        private string GetFileName(HttpPostedFileBase file)
        {
            string guid = Guid.NewGuid().ToString();
            string fileName = guid + Path.GetFileName(file.FileName);

            return fileName;
        }

        private string GetDirectory(HttpPostedFileBase file, string physicalPath)
        {
            this.CreateDirectoryIfNotExist(physicalPath);

            string fileName = this.GetFileName(file);
            string directory = physicalPath + "/" + fileName;

            return directory;
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
