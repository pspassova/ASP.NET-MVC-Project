using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Models.ArticleTests
{
    [TestFixture]
    public class Class_Should
    {
        [Test]
        public void SetIdWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            Article article = new Article { Id = id };

            // Act, Assert
            Assert.AreEqual(id, article.Id);
        }

        [Test]
        public void SetTitleWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            string title = "test title";

            Article article = new Article { Title = title };

            // Act, Assert
            Assert.AreEqual(title, article.Title);
        }

        [Test]
        public void SetContentWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            string content = "test content";

            Article article = new Article { Content = content };

            // Act, Assert
            Assert.AreEqual(content, article.Content);
        }

        [Test]
        public void SetUserIdWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            string userId = "test userId";

            Article article = new Article { UserId = userId };

            // Act, Assert
            Assert.AreEqual(userId, article.UserId);
        }

        [Test]
        public void SetUserWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            var userMock = new Mock<User>();

            Article article = new Article { User = userMock.Object };

            // Act, Assert
            Assert.AreEqual(userMock.Object, article.User);
        }

        [Test]
        public void SetIsDeletedWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            bool isDeleted = true;

            Article article = new Article { IsDeleted = isDeleted };

            // Act, Assert
            Assert.AreEqual(isDeleted, article.IsDeleted);
        }

        [Test]
        public void SetCreatedOnWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            DateTime? createdOn = DateTime.UtcNow;

            Article article = new Article { CreatedOn = createdOn };

            // Act, Assert
            Assert.AreEqual(createdOn, article.CreatedOn);
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

            Article article = new Article { Tags = tags };

            // Act, Assert
            Assert.AreEqual(tags, article.Tags);
        }
    }
}
