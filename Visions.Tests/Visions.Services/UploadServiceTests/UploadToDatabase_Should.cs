using Moq;
using NUnit.Framework;
using System;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services;

namespace Visions.Tests.Visions.Services.UploadServiceTests
{
    [TestFixture]
    public class UploadToDatabase_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenAnItemToUploadIsNotProvided()
        {
            // Arrange
            var dbSetWrapperMock = new Mock<IEfDbSetWrapper<Photo>>();
            var dbContextSaveChangesMock = new Mock<IEfDbContextSaveChanges>();

            UploadService<Photo> UploadService = new UploadService<Photo>(dbSetWrapperMock.Object, dbContextSaveChangesMock.Object);

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => UploadService.UploadToDatabase(null));
        }

        [Test]
        public void InvokeAddMethodFromDbSetWrapper_WhenAnItemToUploadIsProvided()
        {
            // Arrange
            var itemMock = new Mock<Tag>();
            var dbSetWrapperMock = new Mock<IEfDbSetWrapper<Tag>>();
            var dbContextSaveChangesMock = new Mock<IEfDbContextSaveChanges>();

            UploadService<Tag> UploadService = new UploadService<Tag>(dbSetWrapperMock.Object, dbContextSaveChangesMock.Object);

            // Act
            UploadService.UploadToDatabase(itemMock.Object);

            // Assert
            dbSetWrapperMock.Verify(x => x.Add(itemMock.Object), Times.Once);
        }
    }
}
