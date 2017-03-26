using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;
using Visions.Web.Controllers;
using Visions.Web.Models.Account;

namespace Visions.Tests.Visions.Web.Controllers.AccountControllerTests
{
    [TestFixture]
    public class Login_Should
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
            MethodInfo loginMethod = typeof(AccountController)
                .GetMethods()
                .FirstOrDefault(x => x.Name == "Login" && x.GetParameters().Count() == 1);
            Attribute attribute = loginMethod
                .GetCustomAttributes()
                .FirstOrDefault(x => (x as AllowAnonymousAttribute) != null);

            // Assert
            Assert.NotNull(attribute);
        }

        [Test]
        public void SetViewBagReturnUrlKey_WhenReturnUrlIsPassed()
        {
            // Arrange
            string testReturnUrl = "test";

            // Act
            ActionResult result = this.controller.Login(testReturnUrl);

            // Assert
            Assert.IsNotNull(this.controller.ViewBag.ReturnUrl);
        }

        [Test]
        public void RenderDefaultView()
        {
            // Arrange, Act, Assert
            this.controller.WithCallTo(x => x.Login(It.IsAny<string>()))
                .ShouldRenderDefaultView();
        }

        // POST
        [Test]
        public void HaveAllowAnonymusAttribute_WhenTheMethodHandlesPostRequest()
        {
            // Arrange, Act
            MethodInfo loginMethod = typeof(AccountController)
                .GetMethods()
                .FirstOrDefault(x => x.Name == "Login" && x.GetParameters().Count() == 2);
            var attribute = loginMethod
                .GetCustomAttributes()
                .FirstOrDefault(x => (x as AllowAnonymousAttribute) != null);

            // Assert
            Assert.NotNull(attribute);
        }

        [Test]
        public void HaveValidateAntiForgeryAttribute_WhenTheMethodHandlesPostRequest()
        {
            // Arrange, Act
            MethodInfo loginMethod = typeof(AccountController)
                .GetMethods()
                .FirstOrDefault(x => x.Name == "Login" && x.GetParameters().Count() == 2);
            var attribute = loginMethod
                .GetCustomAttributes()
                .FirstOrDefault(x => (x as ValidateAntiForgeryTokenAttribute) != null);

            // Assert
            Assert.NotNull(attribute);
        }

        [Test]
        public void RenderDefaultView_IfModelStateIsInvalid()
        {
            // Arrange
            string modelErrorTestKey = "";
            string modelErrorTestExceptionMessage = "Invalid login attempt.";

            this.controller.ModelState.AddModelError(modelErrorTestKey, modelErrorTestExceptionMessage);

            // Act, Assert
            this.controller.WithCallTo(x => x.Login(It.IsAny<LoginViewModel>(), It.IsAny<string>()))
               .ShouldRenderDefaultView();
        }
    }
}
