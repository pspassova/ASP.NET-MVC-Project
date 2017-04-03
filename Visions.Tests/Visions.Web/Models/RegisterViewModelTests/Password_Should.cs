using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Visions.Web.Models;

namespace Visions.Tests.Visions.Web.Models.RegisterViewModelTests
{
    [TestFixture]
    public class Password_Should
    {
        [Test]
        public void Have_RequiredAttribute()
        {
            // Arrange
            string propertyName = "Password";

            RegisterViewModel registerViewModel = new RegisterViewModel();

            // Act
            bool hasAttribute = registerViewModel.GetType()
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
            string propertyName = "Password";

            RegisterViewModel registerViewModel = new RegisterViewModel();

            // Act
            bool hasAttribute = registerViewModel.GetType()
                .GetProperty(propertyName)
                .GetCustomAttributes(false)
                .Where(p => p.GetType() == typeof(DisplayAttribute))
                .Any();

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void Have_DataTypeAddressAttribute()
        {
            // Arrange
            string propertyName = "Password";

            RegisterViewModel registerViewModel = new RegisterViewModel();

            // Act
            bool hasAttribute = registerViewModel.GetType()
                .GetProperty(propertyName)
                .GetCustomAttributes(false)
                .Where(p => p.GetType() == typeof(DataTypeAttribute))
                .Any();

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void Have_StringLengthAddressAttribute()
        {
            // Arrange
            string propertyName = "Password";

            RegisterViewModel registerViewModel = new RegisterViewModel();

            // Act
            bool hasAttribute = registerViewModel.GetType()
                .GetProperty(propertyName)
                .GetCustomAttributes(false)
                .Where(p => p.GetType() == typeof(StringLengthAttribute))
                .Any();

            // Assert
            Assert.IsTrue(hasAttribute);
        }
    }
}
