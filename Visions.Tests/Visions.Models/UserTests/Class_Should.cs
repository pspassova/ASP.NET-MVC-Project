using NUnit.Framework;
using System.Collections.Generic;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Models.UserTests
{
    [TestFixture]
    public class Class_Should
    {
        [Test]
        public void SetAvatarPathWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            string avatarPath = "test";

            User user = new User { AvatarPath = avatarPath };

            // Act, Assert
            Assert.AreEqual(avatarPath, user.AvatarPath);
        }

        [Test]
        public void SetPinnedArticlesWithTheCorrectValue_WhenTheyAreProvided()
        {
            // Arrange
            ICollection<Article> pinnedArticles = new List<Article>() { new Article() };

            User user = new User { PinnedArticles = pinnedArticles };

            int expectedArticlesCount = 1;

            // Act, Assert
            Assert.AreEqual(expectedArticlesCount, pinnedArticles.Count);
            Assert.AreEqual(pinnedArticles, user.PinnedArticles);
        }

        [Test]
        public void SetPinnedPhotosWithTheCorrectValue_WhenTheyAreProvided()
        {
            // Arrange
            ICollection<Photo> pinnedPhotos = new List<Photo>() { new Photo() };

            User user = new User { PinnedPhotos = pinnedPhotos };

            int expectedPhotosCount = 1;

            // Act, Assert
            Assert.AreEqual(expectedPhotosCount, pinnedPhotos.Count);
            Assert.AreEqual(pinnedPhotos, user.PinnedPhotos);
        }
    }
}
