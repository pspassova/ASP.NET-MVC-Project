using Moq;
using NUnit.Framework;
using System;
using System.Web;
using Visions.Auth;

namespace Visions.Tests.Visoins.Auth.UserProviderTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenHttpContextIsNull()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new UserProvider(null));
        }

        [Test]
        public void CreateAnInstanceOfUserProvider_WhenHttpContextIsProvided()
        {
            // Arrange
            var httpContextMock = new Mock<HttpContextBase>();

            // Act, Assert
            Assert.IsInstanceOf<UserProvider>(new UserProvider(httpContextMock.Object));
        }
    }
}
