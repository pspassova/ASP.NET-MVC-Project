using Moq;
using Ninject.Extensions.Interception;
using NUnit.Framework;
using System;
using Visions.Data.Contracts;
using Visions.Data.Interceptors;

namespace Visions.Tests.Visions.Data.Interceptors.SaveChangesInterceptorTests
{
    [TestFixture]
    public class Intercept_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenInvocationIsNull()
        {
            // Arrange
            var contextMock = new Mock<IVisionsDbContext>();
            SaveChangesInterceptor interceptor = new SaveChangesInterceptor(contextMock.Object);

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => interceptor.Intercept(null));
        }

        [Test]
        public void InvokeProceedMethod_Once_WhenInvocationIsProvided()
        {
            // Arrange
            var contextMock = new Mock<IVisionsDbContext>();
            SaveChangesInterceptor interceptor = new SaveChangesInterceptor(contextMock.Object);

            var invocationMock = new Mock<IInvocation>();

            // Act
            interceptor.Intercept(invocationMock.Object);

            // Assert
            invocationMock.Verify(x => x.Proceed(), Times.Once);
        }

        [Test]
        public void InvokeSaveChangesMethod_Once_WhenInvocationIsProvided()
        {
            // Arrange
            var contextMock = new Mock<IVisionsDbContext>();
            SaveChangesInterceptor interceptor = new SaveChangesInterceptor(contextMock.Object);

            var invocationMock = new Mock<IInvocation>();

            // Act
            interceptor.Intercept(invocationMock.Object);

            // Assert
            contextMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
