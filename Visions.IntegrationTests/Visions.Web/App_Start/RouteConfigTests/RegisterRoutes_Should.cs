using Ninject;
using NUnit.Framework;
using System.Web.Routing;
using Visions.Web;
using Visions.Web.App_Start;

namespace Visions.IntegrationTests.Visions.Web.App_Start.RouteConfigTests
{
    [TestFixture]
    public class RegisterRoutes_Should
    {
        private static IKernel kernel;

        [SetUp]
        public void Setup()
        {
            kernel = NinjectWebCommon.CreateKernel();
        }

        [Test]
        public void SetLowercaseUrlsToTrue_WhenRouteCollectionIsProvided()
        {
            // Arrange
            RouteCollection routes = new RouteCollection();

            // Act
            RouteConfig.RegisterRoutes(routes);

            // Assert
            Assert.That(routes.LowercaseUrls, Is.EqualTo(true));
        }
    }
}
