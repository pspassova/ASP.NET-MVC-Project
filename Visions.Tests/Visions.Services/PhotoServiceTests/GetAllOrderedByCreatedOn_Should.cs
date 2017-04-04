using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services;
using Visions.Services.Enumerations;

namespace Visions.Tests.Visions.Services.PhotoServiceTests
{
    [TestFixture]
    public class GetAllOrderedByCreatedOn_Should
    {
        private PhotoService service;
        private Mock<IEfDbSetWrapper<Photo>> dbSetWrapperMock;

        [SetUp]
        public void Setup()
        {
            this.dbSetWrapperMock = new Mock<IEfDbSetWrapper<Photo>>();

            this.service = new PhotoService(this.dbSetWrapperMock.Object);
        }

        [Test]
        public void ReturnTheCollectionOfPhotosOrderedByDescending_WhenUserIdIsEmptyAndOrderIsDescending()
        {
            // Arrange
            OrderBy order = OrderBy.Descending;

            IQueryable<Photo> expectedPhotos = new List<Photo>()
            {
                new Photo() { CreatedOn = DateTime.UtcNow },
                new Photo() { CreatedOn = DateTime.Now }
            }.OrderByDescending(x => x.CreatedOn)
            .AsQueryable();

            this.dbSetWrapperMock.Setup(x => x.All).Returns(expectedPhotos);

            // Act
            IQueryable<Photo> actualPhotos = this.service.GetAllOrderedByCreatedOn(order, string.Empty);

            // Assert
            CollectionAssert.AreEqual(expectedPhotos, actualPhotos);
        }

        [TestCase(OrderBy.Ascending)]
        [TestCase(null)]
        public void ReturnTheCollectionOfPhotosOrderedByAscending_WhenUserIdIsEmptyAndOrderIsAscendingOrNull(OrderBy? order)
        {
            // Arrange
            IQueryable<Photo> expectedPhotos = new List<Photo>()
            {
                new Photo() { CreatedOn = DateTime.UtcNow },
                new Photo() { CreatedOn = DateTime.Now }
            }.OrderBy(x => x.CreatedOn)
            .AsQueryable();

            this.dbSetWrapperMock.Setup(x => x.All).Returns(expectedPhotos);

            // Act
            IQueryable<Photo> actualPhotos = this.service.GetAllOrderedByCreatedOn(order, string.Empty);

            // Assert
            CollectionAssert.AreEqual(expectedPhotos, actualPhotos);
        }

        [Test]
        public void ReturnTheCollectionOfPhotosFilteredByUserIdAndOrderedByDescending_WhenUserIdIsProvidedAndOrderIsDescending()
        {
            // Arrange
            string userId = "test id";
            OrderBy order = OrderBy.Descending;

            IQueryable<Photo> expectedPhotos = new List<Photo>()
            {
                new Photo() { CreatedOn = DateTime.UtcNow, UserId = userId },
                new Photo() { CreatedOn = DateTime.Now, UserId = userId },
                new Photo() { CreatedOn = DateTime.Now, UserId = null }
            }.Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedOn)
            .AsQueryable();

            this.dbSetWrapperMock.Setup(x => x.All).Returns(expectedPhotos);

            // Act
            IQueryable<Photo> actualPhotos = this.service.GetAllOrderedByCreatedOn(order, userId);

            // Assert
            CollectionAssert.AreEqual(expectedPhotos, actualPhotos);
        }

        [TestCase(OrderBy.Ascending)]
        [TestCase(null)]
        public void ReturnTheCollectionOfPhotoFilteredByUserIdAndOrderedByAscending_WhenUserIdIsProvidedyAndOrderIsAscendingOrNull(OrderBy? order)
        {
            // Arrange
            string userId = "test id";

            IQueryable<Photo> expectedPhotos = new List<Photo>()
            {
                new Photo() { CreatedOn = DateTime.UtcNow, UserId = userId },
                new Photo() { CreatedOn = DateTime.Now, UserId = userId },
                new Photo() { CreatedOn = DateTime.Now, UserId = null }
            }.Where(x => x.UserId == userId)
            .OrderBy(x => x.CreatedOn)
            .AsQueryable();

            this.dbSetWrapperMock.Setup(x => x.All).Returns(expectedPhotos);

            // Act
            IQueryable<Photo> actualPhotos = this.service.GetAllOrderedByCreatedOn(order, userId);

            // Assert
            CollectionAssert.AreEqual(expectedPhotos, actualPhotos);
        }
    }
}
