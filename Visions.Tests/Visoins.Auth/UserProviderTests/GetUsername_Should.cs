using Moq;
using NUnit.Framework;
using System.Web;
using Visions.Auth;
using Visions.Auth.Contracts;

namespace Visions.Tests.Visoins.Auth.UserProviderTests
{
    [TestFixture]
    public class GetUsername_Should
    {
        [TestCase("")]
        [TestCase("test username")]
        public void ReturnTheCorrectUsername(string username)
        {
            // Arrange
            var contextMock = new Mock<HttpContextBase>();
            contextMock.Setup(x => x.User.Identity.Name).Returns(username);

            IUserProvider userProvider = new UserProvider(contextMock.Object);

            // Act, Assert
            Assert.AreEqual(username, userProvider.GetUsername());
        }
    }
}
