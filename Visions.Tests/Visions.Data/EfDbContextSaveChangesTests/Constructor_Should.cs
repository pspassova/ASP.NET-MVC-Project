using Moq;
using NUnit.Framework;
using System;
using Visions.Data;
using Visions.Data.Contracts;

namespace Visions.Tests.Visions.Data.EfDbContextSaveChangesTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumenNullException_WhenDbContextIsNull()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new EfDbContextSaveChanges(null));
        }

        [Test]
        public void CreateAnInstanceOfEfDbContextSaveChanges_WhenDbContextIsProvided()
        {
            // Arrange
            var dbContextMock = new Mock<IEfDbContext>();

            // Act, Assert
            Assert.IsInstanceOf<EfDbContextSaveChanges>(new EfDbContextSaveChanges(dbContextMock.Object));
        }
    }
}
