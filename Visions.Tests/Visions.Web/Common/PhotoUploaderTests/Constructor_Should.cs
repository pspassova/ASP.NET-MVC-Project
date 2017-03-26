using Moq;
using NUnit.Framework;
using System;
using Visions.Auth.Contracts;
using Visions.Helpers.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Web.Common;

namespace Visions.Tests.Visions.Web.Common.PhotoUploaderTests
{
    [TestFixture]
    public class Constructor_Should
    {
        private Mock<IPhotoHelper> photoUploadHelperMock;
        private Mock<IPhotoService> photoServiceMock;
        private Mock<IUploadService<Photo>> uploadPhotoServiceMock;
        private Mock<IUserProvider> userProviderMock;

        [SetUp]
        public void Setup()
        {
            this.photoUploadHelperMock = new Mock<IPhotoHelper>();
            this.photoServiceMock = new Mock<IPhotoService>();
            this.uploadPhotoServiceMock = new Mock<IUploadService<Photo>>();
            this.userProviderMock = new Mock<IUserProvider>();
        }

        [Test]
        public void ThrowArgumentNullException_WhenPhotoUploadHelperIsNull()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new PhotoUploader(
                null,
                this.photoServiceMock.Object,
                this.uploadPhotoServiceMock.Object,
                this.userProviderMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPhotoServiceIsNull()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new PhotoUploader(
                this.photoUploadHelperMock.Object,
                null,
                this.uploadPhotoServiceMock.Object,
                this.userProviderMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUploadPhotoServiceIsNull()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new PhotoUploader(
                this.photoUploadHelperMock.Object,
                this.photoServiceMock.Object,
                null,
                this.userProviderMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUserProviderIsNull()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new PhotoUploader(
                this.photoUploadHelperMock.Object,
                this.photoServiceMock.Object,
                this.uploadPhotoServiceMock.Object,
                null));
        }

        [Test]
        public void ReturnAnInstanceOfPhotoUploader_WhenAllTheParametersAreProvided()
        {
            // Arrange, Act, Assert
            Assert.IsInstanceOf<PhotoUploader>(new PhotoUploader(
                this.photoUploadHelperMock.Object,
                this.photoServiceMock.Object,
                this.uploadPhotoServiceMock.Object,
                this.userProviderMock.Object));
        }
    }
}
