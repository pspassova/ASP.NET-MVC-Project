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
    [TestFixture]
    public class Edit_Should
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
            this.controller.Edit(It.IsAny<Guid>());

            // Assert
            this.photoServiceMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void ReturnHttpNotFoundResult_WhenPassedIdIsNotFound()
        {
            // Arrange, Act
            var result = this.controller.Edit(It.IsAny<Guid>());

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
            this.controller.Edit(It.IsAny<Guid>());

            // Assert
            this.photoConverterMock.Verify(x => x.ConvertToViewModel(It.IsAny<Photo>()), Times.Once);
        }

        [Test]
        public void RenderTheCorrectPartialView_WhenPassedIdIsFound_AndItIsAHttpGetRequest()
        {
            // Arrange
            string partialView = "_EditPhoto";

            var photoMock = new Mock<Photo>();
            this.photoServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(photoMock.Object);

            // Act, Assert
            this.controller.WithCallTo(x => x.Edit(It.IsAny<Guid>())).ShouldRenderPartialView(partialView);
        }

        [Test]
        public void RenderTheCorrectPartialView_WithTheCorrectModel_WhenPassedIdIsFound_AndItIsAHttpGetRequest()
        {
            // Arrange
            string partialView = "_EditPhoto";

            var photoMock = new Mock<Photo>();
            this.photoServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(photoMock.Object);

            var photoViewModelMock = new Mock<PhotoViewModel>();
            this.photoConverterMock.Setup(x => x.ConvertToViewModel(It.IsAny<Photo>())).Returns(photoViewModelMock.Object);

            // Act, Assert
            this.controller.WithCallTo(x => x.Edit(It.IsAny<Guid>()))
                .ShouldRenderPartialView(partialView)
                .WithModel(photoViewModelMock.Object);
        }

        // [HttpPost]
        [Test]
        public void HaveValidateAntiForgeryTokenAttribute()
        {
            // Arrange, Act
            MethodAttributes attribute = typeof(ProfileController).GetMethods()
                .Where(x => x.Name == nameof(ProfileController.Edit))
                .Select(x => x.Attributes)
                .SingleOrDefault(x => x.GetType() == typeof(ValidateAntiForgeryTokenAttribute));

            // Assert
            Assert.IsNotNull(attribute);
        }

        [Test]
        public void SetTempDataSuccessKey_WithCorrectValue_WhenSearchedPhotoIsNotFound()
        {
            // Arrange
            string key = "Success";
            string value = "Edit failed";

            this.photoServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(It.IsAny<Photo>());

            var photoViewModelMock = new Mock<PhotoViewModel>();

            // Act
            ActionResult result = this.controller.Edit(photoViewModelMock.Object);

            // Assert
            Assert.That(this.controller.TempData.ContainsKey(key));
            Assert.That(this.controller.TempData.ContainsValue(value));
        }

        [Test]
        public void RedirectToRoute_WhenSearchedPhotoIsNotFound()
        {
            // Arrange
            this.photoServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(It.IsAny<Photo>());

            var photoViewModelMock = new Mock<PhotoViewModel>();

            // Act
            ActionResult result = this.controller.Edit(photoViewModelMock.Object);

            // Assert
            Assert.IsInstanceOf<RedirectToRouteResult>(result);
        }

        [Test]
        public void RedirectToTheCorrectAction_WhenSearchedPhotoIsNotFound()
        {
            // Arrange
            this.photoServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(It.IsAny<Photo>());

            var photoViewModelMock = new Mock<PhotoViewModel>();

            // Act, Assert
            this.controller.WithCallTo(x => x.Edit(photoViewModelMock.Object))
                .ShouldRedirectTo(c => c.Edit(It.IsAny<Guid>()));
        }

        [Test]
        public void SetTheCorrectPropertiesToPhoto_WhenItIsFound_AndPhotoViewModelIsPassed()
        {
            // Arrange
            Guid testId = Guid.NewGuid();
            string testPath = "test path";
            DateTime testCreatedOnDate = DateTime.UtcNow;

            var photoViewModelMock = new Mock<PhotoViewModel>();
            photoViewModelMock.Object.Id = testId;
            photoViewModelMock.Object.Path = testPath;
            photoViewModelMock.Object.CreatedOn = testCreatedOnDate;

            var photoMock = new Mock<Photo>();
            this.photoServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(photoMock.Object);

            // Act
            this.controller.Edit(photoViewModelMock.Object);

            // Assert
            Assert.AreEqual(photoViewModelMock.Object.Id, photoMock.Object.Id);
            Assert.AreEqual(photoViewModelMock.Object.Path, photoMock.Object.Path);
            Assert.AreEqual(photoViewModelMock.Object.CreatedOn, photoMock.Object.CreatedOn);
        }

        [Test]
        public void InvokeEditMethodFromModifyService_Once_WhenSearchedPhotoIsFound()
        {
            // Arrange
            var photoMock = new Mock<Photo>();
            this.photoServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(photoMock.Object);

            this.modifyPhotoServiceMock.Setup(x => x.Edit(It.IsAny<Photo>())).Verifiable();

            var photoViewModelMock = new Mock<PhotoViewModel>();

            // Act
            this.controller.Edit(photoViewModelMock.Object);

            // Assert
            this.modifyPhotoServiceMock.Verify(x => x.Edit(It.IsAny<Photo>()), Times.Once);
        }

        [Test]
        public void SetTempDataSuccessKey_WithCorrectValue_WhenSearchedPhotoIsFound()
        {
            // Arrange
            string key = "Success";
            string value = "Edit successful";

            var photoMock = new Mock<Photo>();
            this.photoServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(photoMock.Object);

            var photoViewModelMock = new Mock<PhotoViewModel>();

            // Act
            ActionResult result = this.controller.Edit(photoViewModelMock.Object);

            // Assert
            Assert.That(this.controller.TempData.ContainsKey(key));
            Assert.That(this.controller.TempData.ContainsValue(value));
        }

        [Test]
        public void RenderTheCorrectPartialView_WhenPassedIdIsFound_AndItIsAHttpPostRequest()
        {
            // Arrange
            string partialView = "_EditPhoto";

            var photoMock = new Mock<Photo>();
            this.photoServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(photoMock.Object);

            var photoViewModelMock = new Mock<PhotoViewModel>();

            // Act, Assert
            this.controller.WithCallTo(x => x.Edit(photoViewModelMock.Object)).ShouldRenderPartialView(partialView);
        }

        [Test]
        public void RenderTheCorrectPartialView_WithTheCorrectModel_WhenPassedIdIsFound_AndItIsAHttpPostRequest()
        {
            // Arrange
            string partialView = "_EditPhoto";

            var photoMock = new Mock<Photo>();
            this.photoServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(photoMock.Object);

            var photoViewModelMock = new Mock<PhotoViewModel>();

            // Act, Assert
            this.controller.WithCallTo(x => x.Edit(photoViewModelMock.Object))
                .ShouldRenderPartialView(partialView)
                .WithModel(photoViewModelMock.Object);
        }
    }
}
