using NUnit.Framework;
using System.ComponentModel;
using System.Linq;
using Visions.Web.Models;

namespace Visions.Tests.Visions.Web.Models.PhotoViewModelTests
{
    [TestFixture]
    public class UserId_Should
    {
        [Test]
        public void Have_DisplayNameAttribute()
        {
            // Arrange
            string propertyName = "UserId";

            PhotoViewModel photoViewModel = new PhotoViewModel();

            // Act
            bool hasAttribute = photoViewModel.GetType()
                .GetProperty(propertyName)
                .GetCustomAttributes(false)
                .Where(p => p.GetType() == typeof(DisplayNameAttribute))
                .Any();

            // Assert
            Assert.IsTrue(hasAttribute);
        }
    }
}
