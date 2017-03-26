using NUnit.Framework;
using System;
using Visions.Web.Controllers;
using System.Web.Mvc;

namespace Visions.Tests.Visions.Web.Controllers.AccountControllerTests
{
    [TestFixture]
    public class Class_Should
    {
        [Test]
        public void HaveAuthorizeAttribute()
        {
            // Arrange, Act
            Attribute attribute = Attribute.GetCustomAttribute(typeof(AccountController), typeof(AuthorizeAttribute));

            // Assert
            Assert.IsNotNull(attribute);
        }
    }
}
