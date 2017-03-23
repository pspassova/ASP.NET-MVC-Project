using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Mvc;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Services.Enumerations;
using Visions.Web.Models;

namespace Visions.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUploadService<Article> uploadArticleService;
        private readonly IArticleService articleService;

        public HomeController(IUploadService<Article> uploadArticleService, IArticleService articleService)
        {
            this.uploadArticleService = uploadArticleService;
            this.articleService = articleService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<ArticleViewModel> articles = this.articleService.GetAll(article => article.CreatedOn, OrderBy.Descending, ArticleViewModel.FromArticle);

            return this.View(articles);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(string articleTitle, string articleContent)
        {
            string userId = this.User.Identity.GetUserId();
            Article article = this.articleService.Create(articleTitle, articleContent, userId);

            this.uploadArticleService.UploadToDatabase(article);
            this.TempData["Success"] = Resources.Constants.UploadSuccessfulMessage;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }
    }
}