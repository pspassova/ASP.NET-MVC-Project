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
    public class Delete_Should
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
            var exception = Assert.Throws<ArgumentNullException>(() => dbSetWrapper.Delete(null));

            // Assert
            StringAssert.IsMatch("entity", exception.ParamName);
        }
    }
}
