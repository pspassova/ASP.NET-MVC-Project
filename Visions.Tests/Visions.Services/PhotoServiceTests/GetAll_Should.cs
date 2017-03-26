using Moq;
using NUnit.Framework;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services;
using Visions.Services.Contracts;

namespace Visions.Tests.Visions.Services.PhotoServiceTests
{
    [TestFixture]
    public class GetAll_Should
    {
        [Test]
        public void InvokeGetAllMethodFromRepository_WhenPassedUserIdIsNull()
        {
            // Arrange
            var repositoryMock = new Mock<IEfRepository<Photo>>();
            IPhotoService photoService = new PhotoService(repositoryMock.Object);

            // Act
            photoService.GetAll(null);

            // Assert
            repositoryMock.Verify(x => x.GetAll(), Times.Once);
        }
    }
}
