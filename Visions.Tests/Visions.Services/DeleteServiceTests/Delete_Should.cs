using Moq;
using NUnit.Framework;
using System;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services;
using Visions.Services.Contracts;

namespace Visions.Tests.Visions.Services.DeleteServiceTests
{
    [TestFixture]
    public class Delete_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenAnItemToDeleteIsNotProvided()
        {
            // Arrange
            var dbSetWrapperMock = new Mock<IEfDbSetWrapper<Photo>>();
            var dbContextSaveChangesMock = new Mock<IEfDbContextSaveChanges>();

            IDeleteService<Photo> deleteService = new DeleteService<Photo>(dbSetWrapperMock.Object, dbContextSaveChangesMock.Object);

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => deleteService.Delete(null));
        }

        [Test]
        public void InvokeDeleteMethodFromDbSetWrapper_WhenAnItemToDeleteIsProvided()
        {
            // Arrange
            var itemMock = new Mock<Tag>();
            var dbSetWrapperMock = new Mock<IEfDbSetWrapper<Tag>>();
            var dbContextSaveChangesMock = new Mock<IEfDbContextSaveChanges>();

            IDeleteService<Tag> deleteService = new DeleteService<Tag>(dbSetWrapperMock.Object, dbContextSaveChangesMock.Object);

            // Act
            deleteService.Delete(itemMock.Object);

            // Assert
            dbSetWrapperMock.Verify(x => x.Delete(itemMock.Object), Times.Once);
        }
    }
}
