using Moq;
using NUnit.Framework;
using System;
using Visions.Data.Contracts;
using Visions.Data.Interceptors;

namespace Visions.Tests.Visions.Data.Interceptors.SaveChangesInterceptorTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenContextIsNotProvided()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new SaveChangesInterceptor(null));
        }

        [Test]
        public void CreateAnInstanceOfSaveChangesInterceptor()
        {
            // Arrange
            var contextMock = new Mock<IVisionsDbContext>();

            // Act, Assert
            Assert.IsInstanceOf<SaveChangesInterceptor>(new SaveChangesInterceptor(contextMock.Object));
        }
    }
}
