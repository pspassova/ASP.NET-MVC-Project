using Moq;
using NUnit.Framework;
using System;
using Visions.Auth.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Web.Controllers;

namespace Visions.Tests.Visions.Web.Controllers.HomeConrollerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        private Mock<IUploadService<Article>> uploadArticleServiceMock;
        private Mock<IArticleService> articleServiceMock;
        private Mock<IUserProvider> userProviderMock;

        [SetUp]
        public void Setup()
        {
            this.uploadArticleServiceMock = new Mock<IUploadService<Article>>();
            this.articleServiceMock = new Mock<IArticleService>();
            this.userProviderMock = new Mock<IUserProvider>();
        }

        [Test]
        public void ThrowArgumentNullException_WhenUploadArticleServiceIsNotProvided()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new HomeController(
                null,
                this.articleServiceMock.Object,
                this.userProviderMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenArticleServiceIsNotProvided()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new HomeController(
                this.uploadArticleServiceMock.Object,
                null,
                this.userProviderMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUserProviderIsNotProvided()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new HomeController(
                this.uploadArticleServiceMock.Object,
                this.articleServiceMock.Object,
                null));
        }

        [Test]
        public void CreateAnInstanceOfHomeController_WhenAllParametersAreProvided()
        {
            // Arrange, Act, Assert
            Assert.IsInstanceOf<HomeController>(new HomeController(
                this.uploadArticleServiceMock.Object,
                this.articleServiceMock.Object,
                this.userProviderMock.Object));
        }
    }
}
