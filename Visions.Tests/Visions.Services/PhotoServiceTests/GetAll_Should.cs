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
    public class GetAll_Should
    {
        [Test]
        public void GetAllPhotosFromDbSetWrapper()
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
            IQueryable<Photo> actualPhotos = service.GetAll();

            // Assert
            CollectionAssert.AreEquivalent(expectedPhotos, actualPhotos);
        }
    }
}
