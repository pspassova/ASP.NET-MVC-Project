using Moq;
using NUnit.Framework;
using System;
using System.Linq.Expressions;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services;
using Visions.Services.Contracts;

namespace Visions.Tests.Visions.Services.PhotoServiceTests
{
    [TestFixture]
    public class SortByTag_Should
    {
        private IPhotoService photoService;
        private Mock<IEfRepository<Photo>> repositoryMock;

        [SetUp]
        public void Setup()
        {
            this.repositoryMock = new Mock<IEfRepository<Photo>>();

            this.photoService = new PhotoService(repositoryMock.Object);
        }

        [Test]
        public void InvokeGetAllMethodFromRepository_WhenPassedTagIsNullAndPassedUserIdIsEmpty()
        {
            // Arrange, Act
            this.photoService.SortByTag(null, string.Empty);

            // Assert
            this.repositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void InvokeGetAllFilteredMethodFromRepository_WhenPassedTagIsNullAndUserIdIsProvided()
        {
            // Arrange
            string userId = "test";

            // Act
            this.photoService.SortByTag(null, userId);

            // Assert
            this.repositoryMock.Verify(x => x.GetAll(It.IsAny<Expression<Func<Photo, bool>>>()), Times.Once);
        }

        [Test]
        public void InvokeGetAllMethodFromRepository_WhenATagIsProvidedAndUserIdIsEmpty()
        {
            // Arrange
            var repositoryMock = new Mock<IEfRepository<Photo>>();
            IPhotoService photoService = new PhotoService(repositoryMock.Object);

            string tag = "test";

            // Act
            photoService.SortByTag(tag, string.Empty);

            // Assert
            repositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void InvokeGetAllFilteredMethodFromRepository_WhenBothParametersAreProvided()
        {
            // Arrange
            string tag = "test";
            string userId = "test";

            // Act
            this.photoService.SortByTag(tag, userId);

            // Assert
            this.repositoryMock.Verify(x => x.GetAll(It.IsAny<Expression<Func<Photo, bool>>>()), Times.Once);
        }
    }
}
