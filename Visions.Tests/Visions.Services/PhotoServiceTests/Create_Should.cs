using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services;
using Visions.Services.Contracts;

namespace Visions.Tests.Visions.Services.PhotoServiceTests
{
    [TestFixture]
    public class Create_Should
    {
        private IPhotoService photoService;
        private Mock<IEfDbSetWrapper<Photo>> repositoryMock;

        [SetUp]
        public void Setup()
        {
            this.repositoryMock = new Mock<IEfDbSetWrapper<Photo>>();

            this.photoService = new PhotoService(repositoryMock.Object);
        }

        [Test]
        public void ReturnAnInstanceOfPhoto()
        {
            // Arrange, Act, Assert
            Assert.IsInstanceOf<Photo>(this.photoService.Create(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<ICollection<Tag>>()));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("test userId")]
        public void SetTheCorrectUserId_WhenReturningTheNewPhoto(string userId)
        {
            // Arrange, Act
            Photo photo = this.photoService.Create(userId, It.IsAny<string>(), It.IsAny<ICollection<Tag>>());

            // Assert
            Assert.AreEqual(userId, photo.UserId);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("test path")]
        public void SetTheCorrectPath_WhenReturningTheNewPhoto(string path)
        {
            // Arrange, Act
            Photo photo = this.photoService.Create(It.IsAny<string>(), path, It.IsAny<ICollection<Tag>>());

            // Assert
            Assert.AreEqual(path, photo.Path);
        }

        [Test]
        public void SetTheCorrectTags_WhenReturningTheNewPhoto()
        {
            // Arrange
            var testData = new List<Tag>
            {
                new Mock<Tag>().Object,
                new Mock<Tag>().Object
            };

            // Act
            Photo photo = this.photoService.Create(It.IsAny<string>(), It.IsAny<string>(), testData);

            // Assert
            Assert.AreEqual(testData, photo.Tags);
        }
    }
}
