using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Models.ArticleTests
{
    [TestFixture]
    public class Title_Should
    {
        [Test]
        public void Have_RequiredAttribute()
        {
            // Arrange
            Article article = new Article();
            string property = "Title";

            // Act
            bool hasAttribute = article.GetType()
                .GetProperty(property)
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
            Article article = new Article();
            string property = "Title";

            // Act
            bool hasAttribute = article.GetType()
                .GetProperty(property)
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
            Article article = new Article();
            string property = "Title";

            // Act
            MinLengthAttribute attribute = article.GetType()
                .GetProperty(property)
                .GetCustomAttributes(false)
                .Where(p => p.GetType() == typeof(MinLengthAttribute))
                .Select(x => (MinLengthAttribute)x)
                .Single();

            // Assert
            Assert.AreEqual(ValidationConstraints.ArticleTitleMinLength, attribute.Length);
        }
    }
}
