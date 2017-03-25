using NUnit.Framework;
using System;
using System.Web.Mvc;
using Visions.Web.Areas.User.Controllers;

namespace Visions.Tests.Visions.Web.Areas.User.Controllers.ProfileControllerTests
{
    [TestFixture]
    public class Class_Should
    {
        [Test]
        public void HaveAuthorizeAttribute()
        {
            Attribute attribute = Attribute.GetCustomAttribute(typeof(ProfileController), typeof(AuthorizeAttribute));

            Assert.IsNotNull(attribute);
        }
    }
}
