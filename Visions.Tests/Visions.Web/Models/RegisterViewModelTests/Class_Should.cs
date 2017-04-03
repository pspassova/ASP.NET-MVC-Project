using NUnit.Framework;
using Visions.Web.Models;

namespace Visions.Tests.Visions.Web.Models.RegisterViewModelTests
{
    [TestFixture]
    public class Class_Should
    {
        [Test]
        public void SetEmailWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            string email = "test email";

            RegisterViewModel registerViewModel = new RegisterViewModel() { Email = email };

            // Act, Assert
            Assert.AreEqual(email, registerViewModel.Email);
        }

        [Test]
        public void SetPasswordWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            string password = "test password";

            RegisterViewModel registerViewModel = new RegisterViewModel() { Password = password };

            // Act, Assert
            Assert.AreEqual(password, registerViewModel.Password);
        }

        [Test]
        public void SetConfirmPasswordWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            string confirmPassword = "test confirmPassword";

            RegisterViewModel registerViewModel = new RegisterViewModel() { ConfirmPassword = confirmPassword };

            // Act, Assert
            Assert.AreEqual(confirmPassword, registerViewModel.ConfirmPassword);
        }
    }
}
