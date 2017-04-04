using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services;
using Visions.Services.Enumerations;

namespace Visions.Tests.Visions.Services.ArticleServiceTests
{
    [TestFixture]
    public class GetAllOrderedByCreatedOn_Should
    {
        private ArticleService service;
        private Mock<IEfDbSetWrapper<Article>> dbSetWrapperMock;

        [SetUp]
        public void Setup()
        {
            this.dbSetWrapperMock = new Mock<IEfDbSetWrapper<Article>>();

            this.service = new ArticleService(this.dbSetWrapperMock.Object);
        }

        [Test]
        public void ReturnTheCollectionOfArticlesOrderedByDescending_WhenUserIdIsEmptyAndOrderIsDescending()
        {
            // Arrange
            OrderBy order = OrderBy.Descending;

            IQueryable<Article> expectedArticles = new List<Article>()
            {
                new Article() { CreatedOn = DateTime.UtcNow },
                new Article() { CreatedOn = DateTime.Now }
            }.OrderByDescending(x => x.CreatedOn)
            .AsQueryable();

            this.dbSetWrapperMock.Setup(x => x.All).Returns(expectedArticles);

            // Act
            IQueryable<Article> actualArticles = this.service.GetAllOrderedByCreatedOn(order, string.Empty);

            // Assert
            CollectionAssert.AreEqual(expectedArticles, actualArticles);
        }

        [TestCase(OrderBy.Ascending)]
        [TestCase(null)]
        public void ReturnTheCollectionOfArticlesOrderedByAscending_WhenUserIdIsEmptyAndOrderIsAscendingOrNull(OrderBy? order)
        {
            // Arrange
            IQueryable<Article> expectedArticles = new List<Article>()
            {
                new Article() { CreatedOn = DateTime.UtcNow },
                new Article() { CreatedOn = DateTime.Now }
            }.OrderBy(x => x.CreatedOn)
            .AsQueryable();

            this.dbSetWrapperMock.Setup(x => x.All).Returns(expectedArticles);

            // Act
            IQueryable<Article> actualArticles = this.service.GetAllOrderedByCreatedOn(order, string.Empty);

            // Assert
            CollectionAssert.AreEqual(expectedArticles, actualArticles);
        }

        [Test]
        public void ReturnTheCollectionOfArticlesFilteredByUserIdAndOrderedByDescending_WhenUserIdIsProvidedAndOrderIsDescending()
        {
            // Arrange
            string userId = "test id";
            OrderBy order = OrderBy.Descending;

            IQueryable<Article> expectedArticles = new List<Article>()
            {
                new Article() { CreatedOn = DateTime.UtcNow, UserId = userId },
                new Article() { CreatedOn = DateTime.Now, UserId = userId },
                new Article() { CreatedOn = DateTime.Now, UserId = null }
            }.Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedOn)
            .AsQueryable();

            this.dbSetWrapperMock.Setup(x => x.All).Returns(expectedArticles);

            // Act
            IQueryable<Article> actualArticles = this.service.GetAllOrderedByCreatedOn(order, userId);

            // Assert
            CollectionAssert.AreEqual(expectedArticles, actualArticles);
        }

        [TestCase(OrderBy.Ascending)]
        [TestCase(null)]
        public void ReturnTheCollectionOfArticleFilteredByUserIdAndOrderedByAscending_WhenUserIdIsProvidedyAndOrderIsAscendingOrNull(OrderBy? order)
        {
            // Arrange
            string userId = "test id";

            IQueryable<Article> expectedArticles = new List<Article>()
            {
                new Article() { CreatedOn = DateTime.UtcNow, UserId = userId },
                new Article() { CreatedOn = DateTime.Now, UserId = userId },
                new Article() { CreatedOn = DateTime.Now, UserId = null }
            }.Where(x => x.UserId == userId)
            .OrderBy(x => x.CreatedOn)
            .AsQueryable();

            this.dbSetWrapperMock.Setup(x => x.All).Returns(expectedArticles);

            // Act
            IQueryable<Article> actualArticles = this.service.GetAllOrderedByCreatedOn(order, userId);

            // Assert
            CollectionAssert.AreEqual(expectedArticles, actualArticles);
        }
    }
}
