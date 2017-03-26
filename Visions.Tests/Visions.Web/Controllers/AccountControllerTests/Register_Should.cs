using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;
using Visions.Web.Controllers;

namespace Visions.Tests.Visions.Web.Controllers.AccountControllerTests
{
    [TestFixture]
    public class Register_Should
    {
        private AccountController controller;

        [SetUp]
        public void Setup()
        {
            this.controller = new AccountController();
        }

        // GET
        [Test]
        public void HaveAllowAnonymusAttribute_WhenTheMethodHandlesGetRequests()
        {
            // Arrange, Act
            MethodInfo registerMethod = typeof(AccountController)
                .GetMethods()
                .FirstOrDefault(x => x.Name == "Register" && x.GetParameters().Count() == 0);
            Attribute attribute = registerMethod
                .GetCustomAttributes()
                .FirstOrDefault(x => (x as AllowAnonymousAttribute) != null);

            // Assert
            Assert.NotNull(attribute);
        }

        [Test]
        public void RenderDefaultView()
        {
            // Arrange, Act, Assert
            this.controller.WithCallTo(x => x.Register())
                .ShouldRenderDefaultView();
        }

        // POST
        [Test]
        public void HaveAllowAnonymusAttribute_WhenTheMethodHandlesPostRequest()
        {
            // Arrange, Act
            MethodInfo registerMethod = typeof(AccountController)
                .GetMethods()
                .FirstOrDefault(x => x.Name == "Register" && x.GetParameters().Count() == 1);
            var attribute = registerMethod
                .GetCustomAttributes()
                .FirstOrDefault(x => (x as AllowAnonymousAttribute) != null);

            // Assert
            Assert.NotNull(attribute);
        }

        [Test]
        public void HaveValidateAntiForgeryAttribute_WhenTheMethodHandlesPostRequest()
        {
            // Arrange, Act
            MethodInfo registerMethod = typeof(AccountController)
                .GetMethods()
                .FirstOrDefault(x => x.Name == "Register" && x.GetParameters().Count() == 1);
            var attribute = registerMethod
                .GetCustomAttributes()
                .FirstOrDefault(x => (x as ValidateAntiForgeryTokenAttribute) != null);

            // Assert
            Assert.NotNull(attribute);
        }
    }
}
