using Moq;
using NUnit.Framework;
using System;
using System.Data.Entity;
using Visions.Data;
using Visions.Data.Contracts;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Data.GenericRepositoryTests
{
    [TestFixture]
    public class GetById_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenEntityIsNull()
        {
            // Arange
            var dbSetMock = new Mock<DbSet<User>>();
            var contextMock = new Mock<IEfDbContext>();
            contextMock.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);

            IEfDbSetWrapper<User> dbSetWrapper = new EfDbSetWrapper<User>(contextMock.Object);

            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => dbSetWrapper.GetById(null));

            // Assert
            StringAssert.IsMatch("id", exception.ParamName);
        }

        [Test]
        public void InvokeFindMethod_WhenValidIdIsProvided()
        {
            // Arrange
            var dbSetMock = new Mock<DbSet<User>>();
            dbSetMock.Setup(x => x.Find(It.IsAny<Guid>())).Returns<User>(null);

            var contextMock = new Mock<IEfDbContext>();
            contextMock.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);

            IEfDbSetWrapper<User> dbSetWrapper = new EfDbSetWrapper<User>(contextMock.Object);

            // Act
            Guid validDataModelId = Guid.NewGuid();
            var actualDataModel = dbSetWrapper.GetById(validDataModelId);

            // Assert
            dbSetMock.Verify(x => x.Find(validDataModelId), Times.Once());
        }
    }
}
