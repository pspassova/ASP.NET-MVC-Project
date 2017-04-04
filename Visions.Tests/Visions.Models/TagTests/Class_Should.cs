using NUnit.Framework;
using System;
using System.Collections.Generic;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Models.TagTests
{
    [TestFixture]
    public class Class_Should
    {
        [Test]
        public void SetIdWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            Tag tag = new Tag { Id = id };

            // Act, Assert
            Assert.AreEqual(id, tag.Id);
        }

        [Test]
        public void SetTextWithTheCorrectValue_WhenItIsProvided()
        {
            // Arrange
            string text = "test";

            Tag tag = new Tag { Text = text };

            // Act, Assert
            Assert.AreEqual(text, tag.Text);
        }

        [Test]
        public void SetPhotosWithTheCorrectValue_WhenTheyAreProvided()
        {
            // Arrange
            ICollection<Photo> photos = new List<Photo>() { new Photo() };

            Tag tag = new Tag { Photos = photos };

            int expectedPhotosCount = 1;

            // Act, Assert
            Assert.AreEqual(expectedPhotosCount, photos.Count);
            Assert.AreEqual(photos, tag.Photos);
        }
    }
}
