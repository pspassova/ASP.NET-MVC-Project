using NUnit.Framework;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Models.ArticleTests
{
    [TestFixture]
    public class IsDeleted_Should
    {
        private const string PropertyName = "IsDeleted";

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
    }
}
