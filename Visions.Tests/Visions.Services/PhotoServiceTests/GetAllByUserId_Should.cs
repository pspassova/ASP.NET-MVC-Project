using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services;

namespace Visions.Tests.Visions.Services.PhotoServiceTests
{
    [TestFixture]
    public class GetAllByUserId_Should
    {
        [Test]
        public void ReturnAllPhotosFromDbSetWrapper_WhenUserIdIsEmpty()
        {
            // Arrange
            var dbSetWrapperMock = new Mock<IEfDbSetWrapper<Photo>>();
            IQueryable<Photo> expectedPhotos = new List<Photo>()
            {
                new Photo(),
                new Photo()
            }.AsQueryable();

            dbSetWrapperMock.Setup(x => x.All).Returns(expectedPhotos);

            PhotoService service = new PhotoService(dbSetWrapperMock.Object);

            // Act
            IQueryable<Photo> actualPhotos = service.GetAllByUserId(string.Empty);

            // Assert
            CollectionAssert.AreEqual(expectedPhotos, actualPhotos);
        }

        [Test]
        public void ReturnAllPhotosFromDbSetWrapperFilteredByUserId_WhenUserIdIsProvided()
        {
            // Arrange
            string userId = "test id";

            var dbSetWrapperMock = new Mock<IEfDbSetWrapper<Photo>>();
            IQueryable<Photo> expectedPhotos = new List<Photo>()
            {
                new Photo() { UserId = userId },
                new Photo()
            }.Where(x => x.UserId == userId)
            .AsQueryable();

            dbSetWrapperMock.Setup(x => x.All).Returns(expectedPhotos);

            PhotoService service = new PhotoService(dbSetWrapperMock.Object);

            // Act
            IQueryable<Photo> actualPhotos = service.GetAllByUserId(userId);

            // Assert
            CollectionAssert.AreEqual(expectedPhotos, actualPhotos);
        }
    }
}
