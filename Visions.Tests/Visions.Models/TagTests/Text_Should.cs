using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Models.TagTests
{
    [TestFixture]
    public class Text_Should
    {
        [Test]
        public void BeAProperty_InTagClass()
        {
            // Arrange
            string propertyName = "Text";

            Tag tag = new Tag();

            // Act
            var actualResult = tag.GetType().GetProperty(propertyName).Name;

            // Assert
            Assert.AreEqual(propertyName, actualResult);
        }

        [Test]
        public void Have_RequiredAttribute()
        {
            // Arrange
            string propertyName = "Text";

            Tag tag = new Tag();

            // Act
            bool hasAttribute = tag.GetType()
                .GetProperty(propertyName)
                .GetCustomAttributes(false)
                .Where(p => p.GetType() == typeof(RequiredAttribute))
                .Any();

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void Have_MinLengthAttribute()
        {
            // Arrange
            string propertyName = "Text";

            Tag tag = new Tag();

            // Act
            bool hasAttribute = tag.GetType()
                .GetProperty(propertyName)
                .GetCustomAttributes(false)
                .Where(p => p.GetType() == typeof(MinLengthAttribute))
                .Any();

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void Have_MinValueConstraintWithCorrectValue()
        {
            // Arrange
            string propertyName = "Text";

            Tag tag = new Tag();

            // Act
            MinLengthAttribute attribute = tag.GetType()
                .GetProperty(propertyName)
                .GetCustomAttributes(false)
                .Where(p => p.GetType() == typeof(MinLengthAttribute))
                .Select(x => (MinLengthAttribute)x)
                .Single();

            // Assert
            Assert.AreEqual(ValidationConstraints.TagTextMinLength, attribute.Length);
        }
    }
}
