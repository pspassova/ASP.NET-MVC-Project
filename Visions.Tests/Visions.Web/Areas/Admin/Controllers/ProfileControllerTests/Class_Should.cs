using NUnit.Framework;
using System;
using System.Web.Mvc;
using Visions.Web.Areas.Admin.Controllers;

namespace Visions.Tests.Visions.Web.Areas.Admin.Controllers.ProfileControllerTests
{
    [TestFixture]
    public class Class_Should
    {
        [Test]
        public void HaveAuthorizeAttribute()
        {
            // Arrange, Act
            Attribute attribute = Attribute.GetCustomAttribute(typeof(ProfileController), typeof(AuthorizeAttribute));

            // Assert
            Assert.IsNotNull(attribute);
        }
    }
}
