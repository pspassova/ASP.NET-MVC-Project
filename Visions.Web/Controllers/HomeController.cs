using Bytes2you.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Visions.Auth.Contracts;
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
        private readonly IUserProvider userProvider;

        private const int MinuteInSeconds = 60;

        public HomeController(
            IUploadService<Article> uploadArticleService,
            IArticleService articleService,
            IUserProvider userProvider)
        {
            Guard.WhenArgument(uploadArticleService, "uploadArticleService").IsNull().Throw();
            Guard.WhenArgument(articleService, "articleService").IsNull().Throw();
            Guard.WhenArgument(userProvider, "userProvider").IsNull().Throw();

            this.uploadArticleService = uploadArticleService;
            this.articleService = articleService;
            this.userProvider = userProvider;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost, Authorize, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Index(string articleTitle, string articleContent)
        {
            string userId = this.userProvider.GetUserId();
            Article article = this.articleService.Create(articleTitle, articleContent, userId);

            this.uploadArticleService.UploadToDatabase(article);

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult About()
        {
            return this.View();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 5 * MinuteInSeconds)]
        public ActionResult AllArticles()
        {
            IEnumerable<ArticleViewModel> articles = this.articleService.GetAllOrderedByCreatedOn(OrderBy.Descending)
                 .Select(ArticleViewModel.FromArticle)
                 .ToList();

            return this.PartialView("_AllArticles", articles);
        }
    }
}