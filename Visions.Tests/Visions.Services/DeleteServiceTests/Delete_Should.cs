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
            var repositoryMock = new Mock<IEfDbSetWrapper<Photo>>();
            IDeleteService<Photo> deleteService = new DeleteService<Photo>(repositoryMock.Object);

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => deleteService.Delete(null));
        }

        [Test]
        public void InvokeDeleteMethodFromRepository_WhenAnItemToDeleteIsProvided()
        {
            // Arrange
            var itemMock = new Mock<Tag>();
            var repositoryMock = new Mock<IEfDbSetWrapper<Tag>>();
            IDeleteService<Tag> deleteService = new DeleteService<Tag>(repositoryMock.Object);

            // Act
            deleteService.Delete(itemMock.Object);

            // Assert
            repositoryMock.Verify(x => x.Delete(itemMock.Object), Times.Once);
        }
    }
}
