using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services;
using Visions.Services.Contracts;
using Visions.Services.Enumerations;
using Visions.Web.Models;

namespace Visions.Tests.Visions.Services.ArticleServiceTests
{
    [TestFixture]
    public class GetAllDetailed_Should
    {
        private IArticleService articleService;
        private Mock<IEfRepository<Article>> repositoryMock;

        [SetUp]
        public void Setup()
        {
            this.repositoryMock = new Mock<IEfRepository<Article>>();

            this.articleService = new ArticleService(repositoryMock.Object);
        }

        [TestCase(0)]
        [TestCase(23)]
        public void GetAllItemsFromAllProperty_WhenCallingDetailedGetAllMethod(int itemsCount)
        {
            // Arrange
            IQueryable<Article> testData = new List<Article>(itemsCount).AsQueryable();

            this.repositoryMock.SetupGet(x => x.All).Returns(testData);

            // Act
            IEnumerable<Type> result = this.articleService.GetAll(
                It.IsAny<Expression<Func<Article, DateTime?>>>(),
                It.IsAny<OrderBy>(),
                It.IsAny<Expression<Func<Article, Type>>>());

            // Assert
            Assert.AreEqual(testData.Count(), result.Count());
        }

        [TestCase(null)]
        [TestCase(OrderBy.Ascending)]
        [TestCase(OrderBy.Descending)]
        public void ReturnTheCorrectItems_NoMatterWhatTheOrderIs_WhenOrderByPropertyIsProvided(OrderBy? orderByOrder)
        {
            // Arrange
            var testData = new List<Article>
            {
                new Mock<Article>().Object,
                new Mock<Article>().Object
            }.AsQueryable();

            this.repositoryMock.SetupGet(x => x.All).Returns(testData);

            Expression<Func<Article, DateTime?>> orderByProperty = (Article article) => article.CreatedOn;

            Type expectedType = typeof(Article);

            // Act
            IEnumerable<Article> result = this.articleService.GetAll(
                orderByProperty,
                orderByOrder,
                It.IsAny<Expression<Func<Article, Article>>>());

            // Assert
            Assert.AreEqual(testData.Count(), result.Count());
            CollectionAssert.AllItemsAreInstancesOfType(result, expectedType);
        }

        [Test]
        public void ReturnTheCorrectItems_WhenSelectAsIsNotNull()
        {
            // Arrange
            var testData = new List<Article>
            {
                new Mock<Article>().Object,
                new Mock<Article>().Object
            }.AsQueryable();

            this.repositoryMock.SetupGet(x => x.All).Returns(testData);

            ArticleViewModel testSelectAsType = new ArticleViewModel();
            Expression<Func<Article, ArticleViewModel>> selectAs = (Article article) => testSelectAsType;

            Type expectedType = typeof(ArticleViewModel);

            // Act
            IEnumerable<ArticleViewModel> result = this.articleService.GetAll(
                It.IsAny<Expression<Func<Article, DateTime?>>>(),
                It.IsAny<OrderBy>(),
                selectAs);

            // Assert
            CollectionAssert.AllItemsAreInstancesOfType(result, expectedType);
        }

        [Test]
        public void ReturnTheCorrectItems_WhenSelectAsIsNull()
        {
            // Arrange
            var testData = new List<Article>
            {
                new Mock<Article>().Object,
                new Mock<Article>().Object
            }.AsQueryable();

            this.repositoryMock.SetupGet(x => x.All).Returns(testData);

            Type expectedType = typeof(Article);

            // Act
            IEnumerable<Article> result = this.articleService.GetAll<DateTime?, Article>(
                It.IsAny<Expression<Func<Article, DateTime?>>>(),
                It.IsAny<OrderBy>(),
                null);

            // Assert
            CollectionAssert.AllItemsAreInstancesOfType(result, expectedType);
        }
    }
}
