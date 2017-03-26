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
    public class GetAllWithUserId_Should
    {
        [Test]
        public void InvokeGetAllMethodFromRepository_WhenUserIdIsProvided()
        {
            // Arrange
            var repositoryMock = new Mock<IEfRepository<Photo>>();
            IPhotoService photoService = new PhotoService(repositoryMock.Object);

            string userId = "test";

            // Act
            photoService.GetAll(userId);

            // Assert
            repositoryMock.Verify(x => x.GetAll(It.IsAny<Expression<Func<Photo, bool>>>()), Times.Once);
        }
    }
}
