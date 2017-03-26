//using Microsoft.AspNet.Identity;
//using Moq;
//using NUnit.Framework;
//using System.Security.Principal;
//using System.Web;
//using Visions.Auth;
//using Visions.Auth.Contracts;

//namespace Visions.Tests.Visoins.Auth.UserProviderTests
//{
//    [TestFixture]
//    public class GetUserId_Should
//    {
//        [TestCase("")]
//        [TestCase("test userId")]
//        public void ReturnTheCorrectUserId(string userId)
//        {
//            // Arrange
//            var identityMock = new Mock<IIdentity>();
//            identityMock.Setup(x => x.GetUserId()).Returns(userId);

//            var principalMock = new Mock<IPrincipal>();
//            principalMock.Setup(x => x.Identity).Returns(identityMock.Object);

//            var contextMock = new Mock<HttpContextBase>();
//            contextMock.Setup(x => x.User).Returns(principalMock.Object);

//            IUserProvider userProvider = new UserProvider(contextMock.Object);

//            // Act, Assert
//            Assert.AreEqual(userId, userProvider.GetUserId());
//        }
//    }
//}
