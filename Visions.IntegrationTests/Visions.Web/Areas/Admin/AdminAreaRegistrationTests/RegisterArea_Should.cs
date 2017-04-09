using Ninject;
using NUnit.Framework;
using System.Web.Mvc;
using System.Web.Routing;
using Visions.Web.App_Start;
using Visions.Web.Areas.Admin;

namespace Visions.IntegrationTests.Visions.Web.Areas.Admin.AdminAreaRegistrationTests
{
    [TestFixture]
    public class RegisterArea_Should
    {
        private static IKernel kernel;

        [SetUp]
        public void Setup()
        {
            kernel = NinjectWebCommon.CreateKernel();
        }

        [Test]
        public void SetLowercaseUrlsToTrue_WhenRegistrationContextIsProvided()
        {
            // Arrange
            AdminAreaRegistration registration = new AdminAreaRegistration();
            AreaRegistrationContext context = new AreaRegistrationContext(
                registration.AreaName,
                new RouteCollection());

            // Act
            registration.RegisterArea(context);

            // Assert
            Assert.That(context.Routes.LowercaseUrls, Is.EqualTo(true));
        }
    }
}
