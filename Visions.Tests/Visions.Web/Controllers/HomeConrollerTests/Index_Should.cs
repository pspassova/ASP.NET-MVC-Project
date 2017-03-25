using Moq;
using NUnit.Framework;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;
using Visions.Auth.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Web.Controllers;

namespace Visions.Tests.Visions.Web.Controllers.HomeConrollerTests
{
    [TestFixture]
    public class Index_Should
    {
        private HomeController controller;
        private Mock<IUploadService<Article>> uploadArticleService;
        private Mock<IArticleService> articleServiceMock;
        private Mock<IUserProvider> userProviderMock;

        [SetUp]
        public void Setup()
        {
            this.uploadArticleService = new Mock<IUploadService<Article>>();
            this.articleServiceMock = new Mock<IArticleService>();
            this.userProviderMock = new Mock<IUserProvider>();

            this.controller = new HomeController(
                this.uploadArticleService.Object,
                this.articleServiceMock.Object,
                this.userProviderMock.Object);
        }

        //[Test]
        //public void RenderDefaultView_WithTheCorrectModel()
        //{
        //    // Arrange
        //    var articlesViewModelMock = new Mock<IEnumerable<ArticleViewModel>>();

        //    Expression<Func<Article, DateTime?>> createdOn = (Article article) => article.CreatedOn;
        //    OrderBy orderBy = OrderBy.Descending;
        //    Expression<Func<Article, ArticleViewModel>> selectAs = (Article article) => new ArticleViewModel();

        //    this.articleServiceMock.Setup(x => x.GetAll(createdOn, orderBy, selectAs))
        //        .Returns(articleServiceMock.Object);

        //    // Act, Assert
        //    this.controller.WithCallTo(x => x.Index())
        //        .ShouldRenderDefaultView()
        //        .WithModel(articlesViewModelMock.Object);
        //}

        [Test]
        public void HaveAuthorizeAttribute()
        {
            // Arrange, Act
            MethodAttributes attribute = typeof(HomeController).GetMethods()
                .Where(x => x.Name == nameof(HomeController.Index))
                .Select(x => x.Attributes)
                .SingleOrDefault(x => x.GetType() == typeof(AuthorizeAttribute));

            // Assert
            Assert.IsNotNull(attribute);
        }

        [Test]
        public void InvokeGetUserIdMethod_Once()
        {
            // Arrange, Act
            this.controller.Index(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            this.userProviderMock.Verify(x => x.GetUserId(), Times.Once);
        }

        [Test]
        public void InvokeCreateMethod_Once_WhenValidParametersAreProvided()
        {
            // Arrange
            string articleTitle = "title";
            string articleContent = "content";
            string userId = "id";

            this.userProviderMock.Setup(x => x.GetUserId()).Returns(userId);

            var articleMock = new Mock<Article>();
            this.articleServiceMock.Setup(x => x.Create(articleTitle, articleContent, userId))
               .Returns(articleMock.Object);

            // Act
            this.controller.Index(articleTitle, articleContent);

            // Assert
            this.articleServiceMock.Verify(x => x.Create(articleTitle, articleContent, userId), Times.Once);
        }

        [Test]
        public void InvokeUploadToDatabaseMethod_Once_WhenValidParametersAreProvided()
        {
            // Arrange
            string articleTitle = "title";
            string articleContent = "content";
            string userId = "id";

            this.userProviderMock.Setup(x => x.GetUserId()).Returns(userId);

            var articleMock = new Mock<Article>();
            this.articleServiceMock.Setup(x => x.Create(articleTitle, articleContent, userId))
               .Returns(articleMock.Object);

            this.uploadArticleService.Setup(x => x.UploadToDatabase(articleMock.Object)).Verifiable();

            // Act
            this.controller.Index(articleTitle, articleContent);

            // Assert
            this.uploadArticleService.Verify(x => x.UploadToDatabase(articleMock.Object), Times.Once);
        }

        [Test]
        public void RedirectToIndexAction()
        {
            // Arrange, Act, Assert
            this.controller.WithCallTo(x => x.Index(It.IsAny<string>(), It.IsAny<string>()))
                .ShouldRedirectTo(x => x.Index());
        }
    }
}
