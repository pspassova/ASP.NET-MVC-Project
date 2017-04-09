using NUnit.Framework;
using Visions.Web.App_Start;

namespace Visions.Tests.Visions.Web.App_Start.DatabaseConfigTests
{
    [TestFixture]
    public class InitializeDatabase_Should
    {
        [Test]
        public void NotThrow_WhenInvokingSetInitializerAndCreateMethods()
        {
            // Arrange, Act
            DatabaseConfig.InitializeDatabase();

            // Assert
            Assert.DoesNotThrow(() => DatabaseConfig.InitializeDatabase());
        }
    }
}
