using NUnit.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Visions.Web.Models;

namespace Visions.Tests.Visions.Web.Models.PhotoViewModelTests
{
    [TestFixture]
    public class CreatedOn_Should
    {
        [Test]
        public void Have_DisplayNameAttribute()
        {
            // Arrange
            string propertyName = "CreatedOn";

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

        [Test]
        public void Have_DisplayFormatAttribute()
        {
            // Arrange
            string propertyName = "CreatedOn";

            PhotoViewModel photoViewModel = new PhotoViewModel();

            // Act
            bool hasAttribute = photoViewModel.GetType()
                .GetProperty(propertyName)
                .GetCustomAttributes(false)
                .Where(p => p.GetType() == typeof(DisplayFormatAttribute))
                .Any();

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void Have_DataTypeAttribute()
        {
            // Arrange
            string propertyName = "CreatedOn";

            PhotoViewModel photoViewModel = new PhotoViewModel();

            // Act
            bool hasAttribute = photoViewModel.GetType()
                .GetProperty(propertyName)
                .GetCustomAttributes(false)
                .Where(p => p.GetType() == typeof(DataTypeAttribute))
                .Any();

            // Assert
            Assert.IsTrue(hasAttribute);
        }
    }
}
