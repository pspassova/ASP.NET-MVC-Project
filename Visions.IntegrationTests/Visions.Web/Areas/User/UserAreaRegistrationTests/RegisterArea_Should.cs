using Ninject;
using NUnit.Framework;
using System.Web.Mvc;
using System.Web.Routing;
using Visions.Web.App_Start;
using Visions.Web.Areas.User;

namespace Visions.IntegrationTests.Visions.Web.Areas.User.UserAreaRegistrationTests
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
            UserAreaRegistration registration = new UserAreaRegistration();
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
