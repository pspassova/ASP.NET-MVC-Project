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
            var repositoryMock = new Mock<IEfRepository<Photo>>();
            UploadService<Photo> UploadService = new UploadService<Photo>(repositoryMock.Object);

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => UploadService.UploadToDatabase(null));
        }

        [Test]
        public void InvokeAddMethodFromRepository_WhenAnItemToUploadIsProvided()
        {
            // Arrange
            var itemMock = new Mock<Tag>();
            var repositoryMock = new Mock<IEfRepository<Tag>>();
            UploadService<Tag> UploadService = new UploadService<Tag>(repositoryMock.Object);

            // Act
            UploadService.UploadToDatabase(itemMock.Object);

            // Assert
            repositoryMock.Verify(x => x.Add(itemMock.Object), Times.Once);
        }
    }
}
