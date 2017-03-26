using Moq;
using NUnit.Framework;
using System;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services;

namespace Visions.Tests.Visions.Services.PhotoServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPhotoRepositoryIsNotProvided()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new PhotoService(null));
        }

        [Test]
        public void CreateAnInstanceOfPhotoService_WhenPhotoRepositoryIsProvided()
        {
            // Arrange
            var repositoryMock = new Mock<IEfRepository<Photo>>();

            // Act, Assert
            Assert.IsInstanceOf<PhotoService>(new PhotoService(repositoryMock.Object));
        }
    }
}
