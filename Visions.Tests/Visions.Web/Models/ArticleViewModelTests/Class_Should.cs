using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Visions.Models.Models;
using Visions.Web.Models;

namespace Visions.Tests.Visions.Web.Models.ArticleViewModelTests
{
    [TestFixture]
    public class Class_Should
    {
        [Test]
        public void SetIdWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            ArticleViewModel articleViewModel = new ArticleViewModel { Id = id };

            // Act, Assert
            Assert.AreEqual(id, articleViewModel.Id);
        }

        [Test]
        public void SetTitleWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            string title = "test title";

            ArticleViewModel articleViewModel = new ArticleViewModel { Title = title };

            // Act, Assert
            Assert.AreEqual(title, articleViewModel.Title);
        }

        [Test]
        public void SetContentWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            string content = "test content";

            ArticleViewModel articleViewModel = new ArticleViewModel { Content = content };

            // Act, Assert
            Assert.AreEqual(content, articleViewModel.Content);
        }

        [Test]
        public void SetUserIdWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            string userId = "test userId";

            ArticleViewModel articleViewModel = new ArticleViewModel { UserId = userId };

            // Act, Assert
            Assert.AreEqual(userId, articleViewModel.UserId);
        }

        [Test]
        public void SetIsDeletedWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            bool isDeleted = true;

            ArticleViewModel articleViewModel = new ArticleViewModel { IsDeleted = isDeleted };

            // Act, Assert
            Assert.AreEqual(isDeleted, articleViewModel.IsDeleted);
        }

        [Test]
        public void SetCreatedOnWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            DateTime? createdOn = DateTime.UtcNow;

            ArticleViewModel articleViewModel = new ArticleViewModel { CreatedOn = createdOn };

            // Act, Assert
            Assert.AreEqual(createdOn, articleViewModel.CreatedOn);
        }

        [Test]
        public void SetTagsWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            ICollection<Tag> tags = new List<Tag>()
            {
                new Mock<Tag>().Object,
                new Mock<Tag>().Object
            };

            ArticleViewModel articleViewModel = new ArticleViewModel { Tags = tags };

            // Act, Assert
            Assert.AreEqual(tags, articleViewModel.Tags);
        }
    }
}
