using NUnit.Framework;
using Visions.Web.Areas.Admin;

namespace Visions.Tests.Visions.Web.Areas.Admin.AdminAreaRegistrationTests
{
    [TestFixture]
    public class Class_Should
    {
        [Test]
        public void HaveAreaNameProperty_WithTheCorrectName()
        {
            // Arrange
            string correctAreaName = "Admin";

            AdminAreaRegistration registration = new AdminAreaRegistration();

            // Act, Assert
            Assert.AreSame(registration.AreaName, correctAreaName);
        }
    }
}
