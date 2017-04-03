using NUnit.Framework;
using Visions.Web.Areas.User;

namespace Visions.Tests.Visions.Web.Areas.User.UserAreaRegistrationTests
{
    [TestFixture]
    public class Class_Should
    {
        [Test]
        public void HaveAreaNameProperty_WithTheCorrectName()
        {
            // Arrange
            string correctAreaName = "User";

            UserAreaRegistration registration = new UserAreaRegistration();

            // Act, Assert
            Assert.AreSame(registration.AreaName, correctAreaName);
        }
    }
}
