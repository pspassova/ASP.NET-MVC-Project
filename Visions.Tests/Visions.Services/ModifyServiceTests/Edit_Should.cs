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
            var dbSetWrapperMock = new Mock<IEfDbSetWrapper<Photo>>();
            var dbContextSaveChangesMock = new Mock<IEfDbContextSaveChanges>();

            IModifyService<Photo> modifyService = new ModifyService<Photo>(dbSetWrapperMock.Object, dbContextSaveChangesMock.Object);

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => modifyService.Edit(null));
        }

        [Test]
        public void InvokeUpdateMethodFromDbSetWrapper_WhenAnItemToEditIsProvided()
        {
            // Arrange
            var itemMock = new Mock<Tag>();
            var dbSetWrapperMock = new Mock<IEfDbSetWrapper<Tag>>();
            var dbContextSaveChangesMock = new Mock<IEfDbContextSaveChanges>();

            IModifyService<Tag> modifyService = new ModifyService<Tag>(dbSetWrapperMock.Object, dbContextSaveChangesMock.Object);

            // Act
            modifyService.Edit(itemMock.Object);

            // Assert
            dbSetWrapperMock.Verify(x => x.Update(itemMock.Object), Times.Once);
        }
    }
}
