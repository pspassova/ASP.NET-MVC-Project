using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Models.ArticleTests
{
    [TestFixture]
    public class UserId_Should
    {
        private const string PropertyName = "UserId";

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
    }
}
