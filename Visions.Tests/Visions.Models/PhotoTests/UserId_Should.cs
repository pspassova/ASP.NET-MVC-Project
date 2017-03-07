using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Models.PhotoTests
{
    [TestFixture]
    public class UserId_Should
    {
        private const string PropertyName = "UserId";

        [Test]
        public void BeAProperty_InPhotoClass()
        {
            // Arrange
            Photo photo = new Photo();

            // Act
            var actualResult = photo.GetType().GetProperty(PropertyName).Name;

            // Assert
            Assert.AreEqual(PropertyName, actualResult);
        }

        [Test]
        public void Have_RequiredAttribute()
        {
            // Arrange
            Photo photo = new Photo();

            // Act
            bool hasAttribute = photo.GetType()
                .GetProperty(PropertyName)
                .GetCustomAttributes(false)
                .Where(p => p.GetType() == typeof(RequiredAttribute))
                .Any();

            // Assert
            Assert.IsTrue(hasAttribute);
        }
    }
}
