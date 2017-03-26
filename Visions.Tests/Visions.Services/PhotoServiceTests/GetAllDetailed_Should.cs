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

namespace Visions.Tests.Visions.Services.PhotoServiceTests
{
    [TestFixture]
    public class GetAllDetailed_Should
    {
        private IPhotoService photoService;
        private Mock<IEfRepository<Photo>> repositoryMock;

        [SetUp]
        public void Setup()
        {
            this.repositoryMock = new Mock<IEfRepository<Photo>>();

            this.photoService = new PhotoService(repositoryMock.Object);
        }

        [TestCase(0)]
        [TestCase(23)]
        public void GetAllItemsFromAllProperty_WhenCallingDetailedGetAllMethod(int itemsCount)
        {
            // Arrange
            IQueryable<Photo> testData = new List<Photo>(itemsCount).AsQueryable();

            this.repositoryMock.SetupGet(x => x.All).Returns(testData);

            // Act
            IEnumerable<Type> result = this.photoService.GetAll(
                It.IsAny<string>(),
                It.IsAny<Expression<Func<Photo, DateTime?>>>(),
                It.IsAny<OrderBy>(),
                It.IsAny<Expression<Func<Photo, Type>>>());

            // Assert
            Assert.AreEqual(testData.Count(), result.Count());
        }

        [TestCase("")]
        [TestCase("test userId")]
        public void ReturnTheCorrectItems_WhenUserIdIsNotNull(string userId)
        {
            // Arrange
            var testData = new List<Photo>
            {
                new Mock<Photo>().Object,
                new Mock<Photo>().Object
            }.AsQueryable();

            this.repositoryMock.SetupGet(x => x.All).Returns(testData);

            Type expectedType = typeof(Photo);

            // Act
            IEnumerable<Photo> result = this.photoService.GetAll<DateTime?, Photo>(
                userId,
                It.IsAny<Expression<Func<Photo, DateTime?>>>(),
                It.IsAny<OrderBy>(),
                It.IsAny<Expression<Func<Photo, Photo>>>());

            // Assert
            CollectionAssert.AllItemsAreInstancesOfType(result, expectedType);
        }

        [TestCase(null)]
        [TestCase(OrderBy.Ascending)]
        [TestCase(OrderBy.Descending)]
        public void ReturnTheCorrectItems_NoMatterWhatTheOrderIs_WhenOrderByPropertyIsProvided(OrderBy? orderByOrder)
        {
            // Arrange
            var testData = new List<Photo>
            {
                new Mock<Photo>().Object,
                new Mock<Photo>().Object
            }.AsQueryable();

            this.repositoryMock.SetupGet(x => x.All).Returns(testData);

            Expression<Func<Photo, DateTime?>> orderByProperty = (Photo Photo) => Photo.CreatedOn;

            Type expectedType = typeof(Photo);

            // Act
            IEnumerable<Photo> result = this.photoService.GetAll(
                It.IsAny<string>(),
                orderByProperty,
                orderByOrder,
                It.IsAny<Expression<Func<Photo, Photo>>>());

            // Assert
            Assert.AreEqual(testData.Count(), result.Count());
            CollectionAssert.AllItemsAreInstancesOfType(result, expectedType);
        }

        [Test]
        public void ReturnTheCorrectItems_WhenSelectAsIsNotNull()
        {
            // Arrange
            var testData = new List<Photo>
            {
                new Mock<Photo>().Object,
                new Mock<Photo>().Object
            }.AsQueryable();

            this.repositoryMock.SetupGet(x => x.All).Returns(testData);

            PhotoViewModel testSelectAsType = new PhotoViewModel();
            Expression<Func<Photo, PhotoViewModel>> selectAs = (Photo Photo) => testSelectAsType;

            Type expectedType = typeof(PhotoViewModel);

            // Act
            IEnumerable<PhotoViewModel> result = this.photoService.GetAll(
                It.IsAny<string>(),
                It.IsAny<Expression<Func<Photo, DateTime?>>>(),
                It.IsAny<OrderBy>(),
                selectAs);

            // Assert
            CollectionAssert.AllItemsAreInstancesOfType(result, expectedType);
        }

        [Test]
        public void ReturnTheCorrectItems_WhenSelectAsIsNull()
        {
            // Arrange
            var testData = new List<Photo>
            {
                new Mock<Photo>().Object,
                new Mock<Photo>().Object
            }.AsQueryable();

            this.repositoryMock.SetupGet(x => x.All).Returns(testData);

            Type expectedType = typeof(Photo);

            // Act
            IEnumerable<Photo> result = this.photoService.GetAll<DateTime?, Photo>(
                It.IsAny<string>(),
                It.IsAny<Expression<Func<Photo, DateTime?>>>(),
                It.IsAny<OrderBy>(),
                null);

            // Assert
            CollectionAssert.AllItemsAreInstancesOfType(result, expectedType);
        }
    }
}
