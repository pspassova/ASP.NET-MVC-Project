using Moq;
using NUnit.Framework;
using System;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services;

namespace Visions.Tests.Visions.Services.ModifyServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenADbSetWrapperIsNotProvided()
        {
            // Arrange
            var dbContextSaveChangesMock = new Mock<IEfDbContextSaveChanges>();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => new ModifyService<Tag>(null, dbContextSaveChangesMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenADbContextSaveChangesIsNotProvided()
        {
            // Arrange
            var dbSetWrapperMock = new Mock<IEfDbSetWrapper<Tag>>();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => new ModifyService<Tag>(dbSetWrapperMock.Object, null));
        }

        [Test]
        public void CreateAnInstanceOfModifyService_WhenBothParametersAreProvided()
        {
            // Arrange
            var dbSetWrapperMock = new Mock<IEfDbSetWrapper<Article>>();
            var dbContextSaveChangesMock = new Mock<IEfDbContextSaveChanges>();

            // Act, Assert
            Assert.IsInstanceOf<ModifyService<Article>>(new ModifyService<Article>(dbSetWrapperMock.Object, dbContextSaveChangesMock.Object));
        }
    }
}
