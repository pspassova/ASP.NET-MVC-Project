using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Visions.Models.Models;
using Visions.Services.Contracts;

namespace Visions.Web.Areas.User.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUploadService<Photo> uploadPhotoService;

        public ProfileController(IUploadService<Photo> uploadPhotoService)
        {
            this.uploadPhotoService = uploadPhotoService;
        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Dashboard(HttpPostedFileBase file)
        {
            if (file!=null && file.ContentLength > 0)
            {
                string username = this.User.Identity.Name.Substring(0, this.User.Identity.Name.IndexOf("@"));
                string directory = Server.MapPath("~/Images/" + username);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string filename = Path.GetFileName(file.FileName);
                string guid = Guid.NewGuid().ToString();
                file.SaveAs(directory + "/" + guid + filename);

                directory = $"/Images/{username}/{guid}{filename}";
                Photo photo = new Photo()
                {
                    Id = Guid.NewGuid(),
                    UserId = "dbbedd4f-39b2-4e0d-a4d2-638f96adb1ff",
                    Path = directory,
                    Likes = 0
                };

                this.uploadPhotoService.Upload(photo);
                TempData["Success"] = "Upload successful";
            }

            return RedirectToAction("Dashboard");
        }
    }
}