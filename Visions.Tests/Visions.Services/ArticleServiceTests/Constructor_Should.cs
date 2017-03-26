using Moq;
using NUnit.Framework;
using System;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services;

namespace Visions.Tests.Visions.Services.ArticleServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenArticleRepositoryIsNotProvided()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new ArticleService(null));
        }

        [Test]
        public void CreateAnInstanceOfArticleService_WhenArticleRepositoryIsProvided()
        {
            // Arrange
            var repositoryMock = new Mock<IEfRepository<Article>>();

            // Act, Assert
            Assert.IsInstanceOf<ArticleService>(new ArticleService(repositoryMock.Object));
        }
    }
}
