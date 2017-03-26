using Moq;
using NUnit.Framework;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services;
using Visions.Services.Contracts;

namespace Visions.Tests.Visions.Services.ArticleServiceTests
{
    [TestFixture]
    public class Create_Should
    {
        private IArticleService articleService;
        private Mock<IEfRepository<Article>> repositoryMock;

        [SetUp]
        public void Setup()
        {
            this.repositoryMock = new Mock<IEfRepository<Article>>();

            this.articleService = new ArticleService(repositoryMock.Object);
        }

        [Test]
        public void CreateAnInstanceOfArticle()
        {
            // Arrange, Act
            Article result = articleService.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            // Assert
            Assert.IsInstanceOf<Article>(result);
        }

        [TestCase("")]
        [TestCase("test title")]
        public void SetTheCorrectTitleToTheReturnedArticle_WhenTitleIsProvided(string title)
        {
            Article result = this.articleService.Create(title, It.IsAny<string>(), It.IsAny<string>());

            // Assert
            Assert.AreEqual(title, result.Title);
        }

        [TestCase("")]
        [TestCase("test content")]
        public void SetTheCorrectContentToTheReturnedArticle_WhenContentIsProvided(string content)
        {
            Article result = this.articleService.Create(It.IsAny<string>(), content, It.IsAny<string>());

            // Assert
            Assert.AreEqual(content, result.Content);
        }

        [TestCase("")]
        [TestCase("test userId")]
        public void SetTheCorrectUserIdToTheReturnedArticle_WhenUserIdIsProvided(string userId)
        {
            Article result = this.articleService.Create(It.IsAny<string>(), It.IsAny<string>(), userId);

            // Assert
            Assert.AreEqual(userId, result.UserId);
        }
    }
}
