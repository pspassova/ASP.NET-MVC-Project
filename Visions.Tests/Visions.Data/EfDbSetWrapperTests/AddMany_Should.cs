using Moq;
using NUnit.Framework;
using System;
using System.Data.Entity;
using Visions.Data;
using Visions.Data.Contracts;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Data.EfDbSetWrapperTests
{
    [TestFixture]
    public class AddMany_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenEntitiesAreNull()
        {
            // Arange
            var dbSetMock = new Mock<DbSet<User>>();
            var contextMock = new Mock<IEfDbContext>();
            contextMock.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);

            IEfDbSetWrapper<User> dbSetWrapper = new EfDbSetWrapper<User>(contextMock.Object);

            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => dbSetWrapper.AddMany(null));

            // Assert
            StringAssert.IsMatch("entities", exception.ParamName);
        }
    }
}
