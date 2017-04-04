using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Models.PhotoTests
{
    [TestFixture]
    public class Class_Should
    {
        [Test]
        public void SetIdWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            Photo photo = new Photo { Id = id };

            // Act, Assert
            Assert.AreEqual(id, photo.Id);
        }

        [Test]
        public void SetUserIdWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            string userId = "test userId";

            Photo photo = new Photo { UserId = userId };

            // Act, Assert
            Assert.AreEqual(userId, photo.UserId);
        }

        [Test]
        public void SetUserWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            var userMock = new Mock<User>();

            Photo photo = new Photo { User = userMock.Object };

            // Act, Assert
            Assert.AreEqual(userMock.Object, photo.User);
        }

        [Test]
        public void SetPathWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            string path = "test path";

            Photo photo = new Photo { Path = path };

            // Act, Assert
            Assert.AreEqual(path, photo.Path);
        }

        [Test]
        public void SetLikesWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            int testLikes = 5;

            Photo photo = new Photo { Likes = testLikes };

            // Act, Assert
            Assert.AreEqual(testLikes, photo.Likes);
        }

        [Test]
        public void SetIsDeletedWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            bool isDeleted = true;

            Photo photo = new Photo { IsDeleted = isDeleted };

            // Act, Assert
            Assert.AreEqual(isDeleted, photo.IsDeleted);
        }

        [Test]
        public void SetCreatedOnWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            DateTime? createdOn = DateTime.UtcNow;

            Photo photo = new Photo { CreatedOn = createdOn };

            // Act, Assert
            Assert.AreEqual(createdOn, photo.CreatedOn);
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

            Photo photo = new Photo { Tags = tags };

            // Act, Assert
            Assert.AreEqual(tags, photo.Tags);
        }
    }
}
