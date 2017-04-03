using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Visions.Web.Models;

namespace Visions.Tests.Visions.Web.Models.PhotoViewModelTests
{
    [TestFixture]
    public class Path_Should
    {
        [Test]
        public void Have_RequiredAttribute()
        {
            // Arrange
            string propertyName = "Path";

            PhotoViewModel photoViewModel = new PhotoViewModel();

            // Act
            bool hasAttribute = photoViewModel.GetType()
                .GetProperty(propertyName)
                .GetCustomAttributes(false)
                .Where(p => p.GetType() == typeof(RequiredAttribute))
                .Any();

            // Assert
            Assert.IsTrue(hasAttribute);
        }
    }
}
