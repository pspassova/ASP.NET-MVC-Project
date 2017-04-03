using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;
using Visions.Auth.Contracts;
using Visions.Models.Models;
using Visions.Web.Controllers;
using Visions.Web.Models;

namespace Visions.Tests.Visions.Web.Controllers.AccountControllerTests
{
    [TestFixture]
    public class Register_Should
    {
        private AccountController controller;
        private Mock<ISignInService> signInServiceMock;
        private Mock<IUserService> userServiceMock;

        [SetUp]
        public void Setup()
        {
            this.signInServiceMock = new Mock<ISignInService>();
            this.userServiceMock = new Mock<IUserService>();

            this.controller = new AccountController(this.signInServiceMock.Object, this.userServiceMock.Object);
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

        [Test]
        public void InvokeCreateAsyncMethod_WithoutSettingErrorsToIdentityResult_WhenModelStateIsValid()
        {
            // Arrange
            var modelMock = new Mock<RegisterViewModel>();
            modelMock.Object.Email = "test";
            modelMock.Object.Password = "test";

            var userMock = new Mock<User>();
            userMock.Object.UserName = modelMock.Object.Email;
            userMock.Object.Email = modelMock.Object.Email;

            var identityResultMock = new Mock<IdentityResult>();
            this.userServiceMock.Setup(x => x.CreateAsync(userMock.Object, modelMock.Object.Password))
                .ReturnsAsync(identityResultMock.Object);

            // Act
            this.controller.Register(modelMock.Object);

            // Assert
            Assert.IsTrue(identityResultMock.Object.Errors.Count() == 0);
        }

        [Test]
        public void RenderDefaultViewWithTheRightModel_WhenModelStateIsInvalid()
        {
            // Arrange
            var modelMock = new Mock<RegisterViewModel>();

            string modelErrorTestKey = "";
            string modelErrorTestExceptionMessage = "Invalid login attempt.";

            this.controller.ModelState.AddModelError(modelErrorTestKey, modelErrorTestExceptionMessage);

            // Act, Assert
            this.controller.WithCallTo(x => x.Register(modelMock.Object))
                .ShouldRenderDefaultView()
                .WithModel(modelMock.Object);
        }
    }
}
