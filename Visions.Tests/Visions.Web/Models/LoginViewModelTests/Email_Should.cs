using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Visions.Web.Models;

namespace Visions.Tests.Visions.Web.Models.LoginViewModelTests
{
    [TestFixture]
    public class Email_Should
    {
        [Test]
        public void Have_RequiredAttribute()
        {
            // Arrange
            string propertyName = "Email";

            LoginViewModel loginViewModel = new LoginViewModel();

            // Act
            bool hasAttribute = loginViewModel.GetType()
                .GetProperty(propertyName)
                .GetCustomAttributes(false)
                .Where(p => p.GetType() == typeof(RequiredAttribute))
                .Any();

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void Have_DisplayAttribute()
        {
            // Arrange
            string propertyName = "Email";

            LoginViewModel loginViewModel = new LoginViewModel();

            // Act
            bool hasAttribute = loginViewModel.GetType()
                .GetProperty(propertyName)
                .GetCustomAttributes(false)
                .Where(p => p.GetType() == typeof(DisplayAttribute))
                .Any();

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void Have_EmailAddressAttribute()
        {
            // Arrange
            string propertyName = "Email";

            LoginViewModel loginViewModel = new LoginViewModel();

            // Act
            bool hasAttribute = loginViewModel.GetType()
                .GetProperty(propertyName)
                .GetCustomAttributes(false)
                .Where(p => p.GetType() == typeof(EmailAddressAttribute))
                .Any();

            // Assert
            Assert.IsTrue(hasAttribute);
        }
    }
}
