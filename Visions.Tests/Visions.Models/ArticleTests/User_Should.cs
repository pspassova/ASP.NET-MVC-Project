using NUnit.Framework;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Models.ArticleTests
{
    [TestFixture]
    public class User_Should
    {
        private const string PropertyName = "User";

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
