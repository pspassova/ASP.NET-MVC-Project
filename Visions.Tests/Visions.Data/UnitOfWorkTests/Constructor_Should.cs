using System;
using Visions.Data;
using NUnit.Framework;
using Visions.Data.Contracts;
using Moq;

namespace Visions.Tests.Visions.Data.UnitOfWorkTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenIVisionsDbContextIsNull()
        {
            // Arrange, Act
            var exception = Assert.Throws<ArgumentNullException>(() => new UnitOfWork(null));

            // Assert
            StringAssert.IsMatch("context", exception.ParamName);
        }

        [Test]
        public void NotThrow_WhenIVisionsDbContextIsValid()
        {
            // Arrange
            var contextMock = new Mock<IVisionsDbContext>();

            // Act
            UnitOfWork unitOfWork = new UnitOfWork(contextMock.Object);

            Assert.That(unitOfWork, Is.Not.Null);
        }
    }
}
