using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Visions.Models.Models;
using Visions.Web.Models;

namespace Visions.Tests.Visions.Web.Models.PhotoViewModelTests
{
    [TestFixture]
    public class Class_Should
    {
        [Test]
        public void SetIdWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            PhotoViewModel photoViewModel = new PhotoViewModel { Id = id };

            // Act, Assert
            Assert.AreEqual(id, photoViewModel.Id);
        }

        [Test]
        public void SetUserIdWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            string userId = "test userId";

            PhotoViewModel photoViewModel = new PhotoViewModel { UserId = userId };

            // Act, Assert
            Assert.AreEqual(userId, photoViewModel.UserId);
        }

        [Test]
        public void SetPathWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            string path = "test path";

            PhotoViewModel photoViewModel = new PhotoViewModel { Path = path };

            // Act, Assert
            Assert.AreEqual(path, photoViewModel.Path);
        }

        [Test]
        public void SetLikesWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            int testLikes = 5;

            PhotoViewModel photoViewModel = new PhotoViewModel { Likes = testLikes };

            // Act, Assert
            Assert.AreEqual(testLikes, photoViewModel.Likes);
        }

        [Test]
        public void SetIsDeletedWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            bool isDeleted = true;

            PhotoViewModel photoViewModel = new PhotoViewModel { IsDeleted = isDeleted };

            // Act, Assert
            Assert.AreEqual(isDeleted, photoViewModel.IsDeleted);
        }

        [Test]
        public void SetCreatedOnWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            DateTime? createdOn = DateTime.UtcNow;

            PhotoViewModel photoViewModel = new PhotoViewModel { CreatedOn = createdOn };

            // Act, Assert
            Assert.AreEqual(createdOn, photoViewModel.CreatedOn);
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

            PhotoViewModel photoViewModel = new PhotoViewModel { Tags = tags };

            // Act, Assert
            Assert.AreEqual(tags, photoViewModel.Tags);
        }
    }
}
