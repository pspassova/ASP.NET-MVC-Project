using NUnit.Framework;
using Visions.Web.Models;

namespace Visions.Tests.Visions.Web.Models.LoginViewModelTests
{
    [TestFixture]
    public class Class_Should
    {
        [Test]
        public void SetEmailWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            string email = "test email";

            LoginViewModel loginViewModel = new LoginViewModel() { Email = email };

            // Act, Assert
            Assert.AreEqual(email, loginViewModel.Email);
        }

        [Test]
        public void SetPasswordWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            string password = "test password";

            LoginViewModel loginViewModel = new LoginViewModel() { Password = password };

            // Act, Assert
            Assert.AreEqual(password, loginViewModel.Password);
        }

        [Test]
        public void SetRememberMeWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            bool rememberMe = true;

            LoginViewModel loginViewModel = new LoginViewModel() { RememberMe = rememberMe };

            // Act, Assert
            Assert.AreEqual(rememberMe, loginViewModel.RememberMe);
        }
    }
}
