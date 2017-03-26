using NUnit.Framework;
using Visions.Web.Controllers;

namespace Visions.Tests.Visions.Web.Controllers.AccountControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateAnInstanceOfAccountController()
        {
            // Arrange, Act, Assert
            Assert.IsInstanceOf<AccountController>(new AccountController());
        }
    }
}
