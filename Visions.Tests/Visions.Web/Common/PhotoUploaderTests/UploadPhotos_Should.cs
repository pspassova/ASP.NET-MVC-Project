using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web;
using Visions.Auth.Contracts;
using Visions.Helpers.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Web.Common;
using Visions.Web.Common.Contracts;

namespace Visions.Tests.Visions.Web.Common.PhotoUploaderTests
{
    [TestFixture]
    public class UploadPhotos_Should
    {
        private IPhotoUploader photoUploader;
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

            this.photoUploader = new PhotoUploader(
                photoUploadHelperMock.Object,
                photoServiceMock.Object,
                uploadPhotoServiceMock.Object,
                userProviderMock.Object);
        }

        [Test]
        public void InvokeGetUserIdMethod_Once()
        {
            // Arrange, Act
            this.photoUploader.UploadPhotos(
                It.IsAny<HttpPostedFileBase>(),
                It.IsAny<string>(),
                It.IsAny<ICollection<Tag>>());

            // Assert
            this.userProviderMock.Verify(x => x.GetUserId(), Times.Once);
        }

        [Test]
        public void InvokeUploadToFileSystemMethod_Once()
        {
            // Arrange, Act
            this.photoUploader.UploadPhotos(
                It.IsAny<HttpPostedFileBase>(),
                It.IsAny<string>(),
                It.IsAny<ICollection<Tag>>());

            // Assert
            this.photoUploadHelperMock.Verify(x => x.UploadToFileSystem(
                It.IsAny<HttpPostedFileBase>(),
                It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void InvokeGetPathForDatabaseMethod_Once()
        {
            // Arrange, Act
            this.photoUploader.UploadPhotos(
                It.IsAny<HttpPostedFileBase>(),
                It.IsAny<string>(),
                It.IsAny<ICollection<Tag>>());

            // Assert
            this.photoUploadHelperMock.Verify(x => x.GetPathForDatabase(), Times.Once);
        }

        [Test]
        public void InvokeCreateMethod_Once()
        {
            // Arrange, Act
            this.photoUploader.UploadPhotos(
                It.IsAny<HttpPostedFileBase>(),
                It.IsAny<string>(),
                It.IsAny<ICollection<Tag>>());

            // Assert
            this.photoServiceMock.Verify(x => x.Create(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<ICollection<Tag>>()), Times.Once);
        }

        [Test]
        public void InvokeUploadToDatabaseMethod_Once()
        {
            // Arrange, Act
            this.photoUploader.UploadPhotos(
                It.IsAny<HttpPostedFileBase>(),
                It.IsAny<string>(),
                It.IsAny<ICollection<Tag>>());

            // Assert
            this.uploadPhotoServiceMock.Verify(x => x.UploadToDatabase(It.IsAny<Photo>()), Times.Once);
        }
    }
}
