using NUnit.Framework;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Visions.Web.Controllers;

namespace Visions.Tests.Visions.Web.Controllers.AccountControllerTests
{
    [TestFixture]
    public class LogOff_Should
    {
        [Test]
        public void HaveValidateAntiForgeryAttribute_WhenTheMethodHandlesPostRequest()
        {
            // Arrange, Act
            MethodInfo logOffMethod = typeof(AccountController)
                .GetMethods()
                .FirstOrDefault(x => x.Name == "LogOff");
            var attribute = logOffMethod
                .GetCustomAttributes()
                .FirstOrDefault(x => (x as ValidateAntiForgeryTokenAttribute) != null);

            // Assert
            Assert.NotNull(attribute);
        }
    }
}
