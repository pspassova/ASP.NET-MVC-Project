﻿using System.Web;

namespace Visions.Web.Helpers.Contracts
{
    public interface IUploadPhoto
    {
        string GetDirectory(HttpPostedFileBase file, string physicalPath);

        void Upload(HttpPostedFileBase file, string directory);
    }
}
