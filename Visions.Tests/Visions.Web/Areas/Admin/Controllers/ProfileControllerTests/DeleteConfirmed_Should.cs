using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;
using Visions.Helpers.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Web.Areas.Admin.Controllers;
using Visions.Web.Common.Contracts;
using Visions.Web.Models;


namespace Visions.Tests.Visions.Web.Areas.Admin.Controllers.ProfileControllerTests
{
    public class DeletingConfirmed_Should
    {
        private ProfileController controller;
        private Mock<HttpServerUtilityBase> serverMock;
        private Mock<IUploadService<Tag>> uploadTagServiceMock;
        private Mock<IModifyService<Photo>> modifyPhotoServiceMock;
        private Mock<IDeleteService<Photo>> deletePhotoServiceMock;
        private Mock<IPhotoService> photoServiceMock;
        private Mock<IPhotoUploader> photoUploaderMock;
        private Mock<IPhotoConverter> photoConverterMock;
        private Mock<ITagsHelper> tagsHelperMock;

        [SetUp]
        public void Setup()
        {
            this.serverMock = new Mock<HttpServerUtilityBase>();
            this.uploadTagServiceMock = new Mock<IUploadService<Tag>>();
            this.modifyPhotoServiceMock = new Mock<IModifyService<Photo>>();
            this.deletePhotoServiceMock = new Mock<IDeleteService<Photo>>();
            this.photoServiceMock = new Mock<IPhotoService>();
            this.photoUploaderMock = new Mock<IPhotoUploader>();
            this.photoConverterMock = new Mock<IPhotoConverter>();
            this.tagsHelperMock = new Mock<ITagsHelper>();

            this.controller = new ProfileController(
                this.serverMock.Object,
                this.uploadTagServiceMock.Object,
                this.modifyPhotoServiceMock.Object,
                this.deletePhotoServiceMock.Object,
                this.photoServiceMock.Object,
                this.photoUploaderMock.Object,
                this.photoConverterMock.Object,
                this.tagsHelperMock.Object);
        }

        [Test]
        public void HaveValidateAntiForgeryTokenAttribute()
        {
            // Arrange, Act
            MethodAttributes attribute = typeof(ProfileController).GetMethods()
                .Where(x => x.Name == nameof(ProfileController.DeletingConfirmed))
                .Select(x => x.Attributes)
                .SingleOrDefault(x => x.GetType() == typeof(ValidateAntiForgeryTokenAttribute));

            // Assert
            Assert.IsNotNull(attribute);
        }

        [Test]
        public void InvokeGetByIdMethod_Once_WhenAnIdIsPassed()
        {
            // Arrange
            this.photoServiceMock.Setup(x => x.GetById(It.IsAny<Guid>()));

            // Act
            this.controller.DeletingConfirmed(It.IsAny<Guid>());

            // Assert
            this.photoServiceMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void ReturnHttpNotFoundResult_WhenPassedIdIsNotFound()
        {
            // Arrange, Act
            var result = this.controller.DeletingConfirmed(It.IsAny<Guid>());

            // Assert
            Assert.IsInstanceOf<HttpNotFoundResult>(result);
        }

        [Test]
        public void InvokeDeleteMethod_Once_WhenPhotoIsFound()
        {
            // Arrange
            var photoMock = new Mock<Photo>();
            this.photoServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(photoMock.Object);

            this.deletePhotoServiceMock.Setup(x => x.Delete(It.IsAny<Photo>())).Verifiable();

            // Act
            this.controller.DeletingConfirmed(It.IsAny<Guid>());

            // Assert
            this.deletePhotoServiceMock.Verify(x => x.Delete(It.IsAny<Photo>()), Times.Once);
        }

        [Test]
        public void RedirectToTheCorrectAction_WhenIdIsPassed()
        {
            // Arrange
            var photoMock = new Mock<Photo>();
            this.photoServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(photoMock.Object);

            // Act, Assert
            this.controller.WithCallTo(x => x.DeletingConfirmed(It.IsAny<Guid>()))
                .ShouldRedirectTo(c => c.Manage);
        }
    }
}
