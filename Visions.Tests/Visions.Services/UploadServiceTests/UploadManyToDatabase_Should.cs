using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services;

namespace Visions.Tests.Visions.Services.UploadServiceTests
{
    [TestFixture]
    public class UploadManyToDatabase_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenItemsToUploadAreNotProvided()
        {
            // Arrange
            var repositoryMock = new Mock<IEfDbSetWrapper<Article>>();
            UploadService<Article> UploadService = new UploadService<Article>(repositoryMock.Object);

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => UploadService.UploadManyToDatabase(null));
        }

        [Test]
        public void InvokeAddManyMethodFromRepository_WhenItemsToUploadAreProvided()
        {
            // Arrange
            var itemsMock = new Mock<IEnumerable<Photo>>();
            var repositoryMock = new Mock<IEfDbSetWrapper<Photo>>();
            UploadService<Photo> UploadService = new UploadService<Photo>(repositoryMock.Object);

            // Act
            UploadService.UploadManyToDatabase(itemsMock.Object);

            // Assert
            repositoryMock.Verify(x => x.AddMany(itemsMock.Object), Times.Once);
        }
    }
}
