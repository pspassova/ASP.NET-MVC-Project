using Moq;
using NUnit.Framework;
using System;
using Visions.Models.Models;
using Visions.Web.Common;
using Visions.Web.Common.Contracts;
using Visions.Web.Models;

namespace Visions.Tests.Visions.Web.Common.PhotoConverterTests
{
    [TestFixture]
    public class ConvertToViewModel_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPhotoIsNull()
        {
            // Arrange
            IPhotoConverter photoConverter = new PhotoConverter();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => photoConverter.ConvertToViewModel(null));
        }

        [Test]
        public void CreateAnInstanceOfPhotoViewModel_WhenPhotoIsProvided()
        {
            // Arrange
            var photoMock = new Mock<Photo>();

            IPhotoConverter photoConverter = new PhotoConverter();

            // Act, Assert
            Assert.IsInstanceOf<PhotoViewModel>(photoConverter.ConvertToViewModel(photoMock.Object));
        }

        [Test]
        public void SetTheCorrectValuesToTheReturnedVieModelProperties_WhenPhotoIsProvided()
        {
            // Arrange
            var photoMock = new Mock<Photo>();
            photoMock.Object.Id = Guid.NewGuid();
            photoMock.Object.UserId = "test userId";
            photoMock.Object.Path = "test path";
            photoMock.Object.Likes = 0;
            photoMock.Object.CreatedOn = DateTime.UtcNow;
            photoMock.Object.IsDeleted = true;

            IPhotoConverter photoConverter = new PhotoConverter();

            // Act
            PhotoViewModel resultViewModel = photoConverter.ConvertToViewModel(photoMock.Object);

            // Assert
            Assert.AreEqual(photoMock.Object.Id, resultViewModel.Id);
            Assert.AreEqual(photoMock.Object.UserId, resultViewModel.UserId);
            Assert.AreEqual(photoMock.Object.Path, resultViewModel.Path);
            Assert.AreEqual(photoMock.Object.Likes, resultViewModel.Likes);
            Assert.AreEqual(photoMock.Object.CreatedOn, resultViewModel.CreatedOn);
            Assert.AreEqual(photoMock.Object.IsDeleted, resultViewModel.IsDeleted);
        }
    }
}
