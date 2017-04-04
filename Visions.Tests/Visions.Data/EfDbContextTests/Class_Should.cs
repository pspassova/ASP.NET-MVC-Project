using Moq;
using NUnit.Framework;
using System.Data.Entity;
using Visions.Data;
using Visions.Data.Contracts;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Data.EfDbContextTests
{
    [TestFixture]
    public class Class_Should
    {
        [Test]
        public void SetArticlesWithTheCorrectValue_WhenTheyAreProvided()
        {
            // Arrange
            var articlesDbSetMock = new Mock<IDbSet<Article>>();

            IVisionsData dbContext = new EfDbContext() { Articles = articlesDbSetMock.Object };

            // Act, Assert
            Assert.AreEqual(articlesDbSetMock.Object, dbContext.Articles);
        }

        [Test]
        public void SetPhotosWithTheCorrectValue_WhenTheyAreProvided()
        {
            // Arrange
            var photosDbSetMock = new Mock<IDbSet<Photo>>();

            IVisionsData dbContext = new EfDbContext() { Photos = photosDbSetMock.Object };

            // Act, Assert
            Assert.AreEqual(photosDbSetMock.Object, dbContext.Photos);
        }

        [Test]
        public void SetTagsWithTheCorrectValue_WhenTheyAreProvided()
        {
            // Arrange
            var tagsDbSetMock = new Mock<IDbSet<Tag>>();

            IVisionsData dbContext = new EfDbContext() { Tags = tagsDbSetMock.Object };

            // Act, Assert
            Assert.AreEqual(tagsDbSetMock.Object, dbContext.Tags);
        }
    }
}
