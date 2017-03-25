using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using Visions.Auth.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Web.Controllers;

namespace Visions.Tests.Visions.Web.Controllers.HomeConrollerTests
{
    [TestFixture]
    public class About_Should
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

        [Test]
        public void RenderDefaultView()
        {
            // Arrange, Act, Assert
            this.controller.WithCallTo(x => x.About()).ShouldRenderDefaultView();
        }
    }
}
