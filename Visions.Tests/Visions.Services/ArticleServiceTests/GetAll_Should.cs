using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services;

namespace Visions.Tests.Visions.Services.ArticleServiceTests
{
    [TestFixture]
    public class GetAll_Should
    {
        [Test]
        public void GetAllArticlesFromDbSetWrapper()
        {
            // Arrange
            var dbSetWrapperMock = new Mock<IEfDbSetWrapper<Article>>();
            IQueryable<Article> expectedArticles = new List<Article>()
            {
                new Article(),
                new Article()
            }.AsQueryable();

            dbSetWrapperMock.Setup(x => x.All).Returns(expectedArticles);

            ArticleService service = new ArticleService(dbSetWrapperMock.Object);

            // Act
            IQueryable<Article> actualArticles = service.GetAll();

            // Assert
            CollectionAssert.AreEquivalent(expectedArticles, actualArticles);
        }
    }
}
