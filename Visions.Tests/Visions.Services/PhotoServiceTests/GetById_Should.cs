using Moq;
using NUnit.Framework;
using System;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services;
using Visions.Services.Contracts;

namespace Visions.Tests.Visions.Services.PhotoServiceTests
{
    [TestFixture]
    public class GetById_Should
    {
        [Test]
        public void InvokeGetByIdMethod_Once()
        {
            // Arrange
            var repositoryMock = new Mock<IEfDbSetWrapper<Photo>>();
            IPhotoService photoService = new PhotoService(repositoryMock.Object);

            // Act
            photoService.GetById(It.IsAny<Guid>());

            // Assert
            repositoryMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
        }
    }
}
