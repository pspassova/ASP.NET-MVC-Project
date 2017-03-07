using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Models.ArticleTests
{
    [TestFixture]
    public class Title_Should
    {
        private const string PropertyName = "Title";

        [Test]
        public void BeAProperty_InArticleClass()
        {
            // Arrange
            Article article = new Article();

            // Act
            var actualResult = article.GetType().GetProperty(PropertyName).Name;

            // Assert
            Assert.AreEqual(PropertyName, actualResult);
        }

        [Test]
        public void Have_RequiredAttribute()
        {
            // Arrange
            Article article = new Article();

            // Act
            bool hasAttribute = article.GetType()
                .GetProperty(PropertyName)
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

            // Act
            bool hasAttribute = article.GetType()
                .GetProperty(PropertyName)
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

            // Act
            MinLengthAttribute attribute = article.GetType()
                .GetProperty(PropertyName)
                .GetCustomAttributes(false)
                .Where(p => p.GetType() == typeof(MinLengthAttribute))
                .Select(x => (MinLengthAttribute)x)
                .Single();

            // Assert
            Assert.AreEqual(ValidationConstraints.ArticleTitleMinLength, attribute.Length);
        }
    }
}
