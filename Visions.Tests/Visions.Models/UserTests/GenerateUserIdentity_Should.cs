using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using System.Security.Claims;
using System.Threading.Tasks;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Models.UserTests
{
    [TestFixture]
    public class GenerateUserIdentity_Should
    {
        [Test]
        public void CreateAValidUserIdentity_WhenUserManagerIsProvided()
        {
            // Arrange
            var userStoreMock = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStoreMock.Object);

            User user = new User();

            // Act
            var actualUserIdentity = user.GenerateUserIdentityAsync(userManager.Object);

            // Assert
            Assert.IsInstanceOf<Task<ClaimsIdentity>>(actualUserIdentity);
        }
    }
}
