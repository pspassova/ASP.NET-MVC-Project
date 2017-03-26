using Moq;
using NUnit.Framework;
using System;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services;
using Visions.Services.Contracts;

namespace Visions.Tests.Visions.Services.ModifyServiceTests
{
    [TestFixture]
    public class Edit_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenAnItemToEditIsNotProvided()
        {
            // Arrange
            var repositoryMock = new Mock<IEfRepository<Photo>>();
            IModifyService<Photo> modifyService = new ModifyService<Photo>(repositoryMock.Object);

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => modifyService.Edit(null));
        }

        [Test]
        public void InvokeUpdateMethodFromRepository_WhenAnItemToEditIsProvided()
        {
            // Arrange
            var itemMock = new Mock<Tag>();
            var repositoryMock = new Mock<IEfRepository<Tag>>();
            IModifyService<Tag> modifyService = new ModifyService<Tag>(repositoryMock.Object);

            // Act
            modifyService.Edit(itemMock.Object);

            // Assert
            repositoryMock.Verify(x => x.Update(itemMock.Object), Times.Once);
        }
    }
}
