using Moq;
using NUnit.Framework;
using System;
using Visions.Data;
using Visions.Data.Contracts;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Data.GenericRepositoryTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenIVisionsDbContextIsNull()
        {
            // Arrange, Act
            var exception = Assert.Throws<ArgumentNullException>(() => new EfDbSetWrapper<User>(null));

            // Assert
            StringAssert.IsMatch("context", exception.ParamName);
        }

        [Test]
        public void CreateAnInstanceOfGenericRepository_WithCorrectDataModel_WhenIVisionsDbContextIsProvided()
        {
            // Arrange
            var contextMock = new Mock<EfDbContext>();
            IEfDbSetWrapper<User> dbSetWrapper = new EfDbSetWrapper<User>(contextMock.Object);

            // Act, Assert
            Assert.IsInstanceOf<EfDbSetWrapper<User>>(dbSetWrapper);
        }
    }
}
