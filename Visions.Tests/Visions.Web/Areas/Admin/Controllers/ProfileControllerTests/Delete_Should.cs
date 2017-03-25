using Moq;
using NUnit.Framework;
using System;
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
    public class Delete_Should
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
        public void InvokeGetByIdMethod_Once_WhenAnIdIsPassed()
        {
            // Arrange
            this.photoServiceMock.Setup(x => x.GetById(It.IsAny<Guid>()));

            // Act
            this.controller.Delete(It.IsAny<Guid>());

            // Assert
            this.photoServiceMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void ReturnHttpNotFoundResult_WhenPassedIdIsNotFound()
        {
            // Arrange, Act
            var result = this.controller.Delete(It.IsAny<Guid>());

            // Assert
            Assert.IsInstanceOf<HttpNotFoundResult>(result);
        }

        [Test]
        public void InvokeConvertToViewModelMethod_Once_WhenPassedIdIsFound()
        {
            // Arrange
            var photoMock = new Mock<Photo>();
            this.photoServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(photoMock.Object);

            this.photoConverterMock.Setup(x => x.ConvertToViewModel(It.IsAny<Photo>()));

            // Act
            this.controller.Delete(It.IsAny<Guid>());

            // Assert
            this.photoConverterMock.Verify(x => x.ConvertToViewModel(It.IsAny<Photo>()), Times.Once);
        }

        [Test]
        public void RenderTheCorrectPartialView_WhenPassedIdIsFound()
        {
            // Arrange
            string partialView = "_DeletePhoto";

            var photoMock = new Mock<Photo>();
            this.photoServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(photoMock.Object);

            // Act, Assert
            this.controller.WithCallTo(x => x.Delete(It.IsAny<Guid>())).ShouldRenderPartialView(partialView);
        }

        [Test]
        public void RenderTheCorrectPartialView_WithTheCorrectModel_WhenPassedIdIsFound()
        {
            // Arrange
            string partialView = "_DeletePhoto";

            var photoMock = new Mock<Photo>();
            this.photoServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(photoMock.Object);

            var photoViewModelMock = new Mock<PhotoViewModel>();
            this.photoConverterMock.Setup(x => x.ConvertToViewModel(It.IsAny<Photo>())).Returns(photoViewModelMock.Object);

            // Act, Assert
            this.controller.WithCallTo(x => x.Delete(It.IsAny<Guid>()))
                .ShouldRenderPartialView(partialView)
                .WithModel<PhotoViewModel>(photoViewModelMock.Object);
        }
    }
}
