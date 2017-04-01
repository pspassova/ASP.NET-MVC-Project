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
            var dbSetWrapperMock = new Mock<IEfDbSetWrapper<Article>>();
            var dbContextSaveChangesMock = new Mock<IEfDbContextSaveChanges>();

            UploadService<Article> UploadService = new UploadService<Article>(dbSetWrapperMock.Object, dbContextSaveChangesMock.Object);

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => UploadService.UploadManyToDatabase(null));
        }

        [Test]
        public void InvokeAddManyMethodFromDbSetWrapper_WhenItemsToUploadAreProvided()
        {
            // Arrange
            var itemsMock = new Mock<IEnumerable<Photo>>();
            var dbSetWrapperMock = new Mock<IEfDbSetWrapper<Photo>>();
            var dbContextSaveChangesMock = new Mock<IEfDbContextSaveChanges>();

            UploadService<Photo> UploadService = new UploadService<Photo>(dbSetWrapperMock.Object, dbContextSaveChangesMock.Object);

            // Act
            UploadService.UploadManyToDatabase(itemsMock.Object);

            // Assert
            dbSetWrapperMock.Verify(x => x.AddMany(itemsMock.Object), Times.Once);
        }
    }
}
