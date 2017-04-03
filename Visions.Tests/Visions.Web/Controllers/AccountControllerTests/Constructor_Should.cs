using Moq;
using NUnit.Framework;
using System;
using Visions.Auth.Contracts;
using Visions.Web.Controllers;

namespace Visions.Tests.Visions.Web.Controllers.AccountControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenSignInServiceIsNotPassed()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => new AccountController(null, userServiceMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUserServiceIsNotPassed()
        {
            // Arrange
            var signInServiceMock = new Mock<ISignInService>();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => new AccountController(signInServiceMock.Object, null));
        }

        [Test]
        public void CreateAnInstanceOfAccountController_WhenBothParametersAreProvided()
        {
            // Arrange
            var signInServiceMock = new Mock<ISignInService>();
            var userServiceMock = new Mock<IUserService>();

            // Act, Assert
            Assert.IsInstanceOf<AccountController>(new AccountController(
                signInServiceMock.Object,
                userServiceMock.Object));
        }
    }
}
