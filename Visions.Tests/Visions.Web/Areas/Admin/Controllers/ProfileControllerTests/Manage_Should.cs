using Moq;
using NUnit.Framework;
using System.Collections.Generic;
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
    public class Manage_Should
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
        public void RenderDefaultView()
        {
            // Arrange, Act, Assert
            this.controller.WithCallTo(x => x.Manage()).ShouldRenderDefaultView();
        }

        [Test]
        public void RenderDefaultView_WithIEnumerableOfPhotoViewModel()
        {
            // Arrange, Act, Assert
            this.controller.WithCallTo(x => x.Manage())
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<PhotoViewModel>>();
        }

        [Test]
        public void RedirectToTheCorrectAction_WhenPassedFileIsNull()
        {
            // Arrange, Act, Assert
            this.controller.WithCallTo(x => x.Manage(null, It.IsAny<string>()))
                .ShouldRedirectTo(c => c.Manage());
        }

        [Test]
        public void RedirectToTheCorrectAction_WhenPassedFileIsNotNull()
        {
            // Arrange
            var fileMock = new Mock<HttpPostedFileBase>();

            // Act, Assert
            this.controller.WithCallTo(x => x.Manage(fileMock.Object, It.IsAny<string>()))
                .ShouldRedirectTo(c => c.Manage());
        }

        [Test]
        public void SetTempDataSuccessKey_WithCorrectValue_WhenPassedFileIsNull()
        {
            // Arrange
            string key = "Success";
            string value = "Upload failed";

            // Act
            ActionResult result = this.controller.Manage(null, It.IsAny<string>());

            // Assert
            Assert.That(this.controller.TempData.ContainsKey(key));
            Assert.That(this.controller.TempData.ContainsValue(value));
        }

        [Test]
        public void InvokeCreateTagsMethod_Once_WhenPassedTagsAreNotNull()
        {
            // Arrange
            string passedTags = "tags";
            this.tagsHelperMock.Setup(x => x.CreateTags(passedTags));

            this.serverMock.Setup(x => x.MapPath(It.IsAny<string>())).Returns((string s) => s);

            var fileMock = new Mock<HttpPostedFileBase>();

            // Act
            this.controller.Manage(fileMock.Object, passedTags);

            // Assert
            this.tagsHelperMock.Verify(x => x.CreateTags(passedTags), Times.Once);
        }

        [Test]
        public void InvokeUploadManyToDatabaseMethod_Once_WhenPassedTagsAreNotNull()
        {
            // Arrange
            string passedTags = "tags";
            this.uploadTagServiceMock.Setup(x => x.UploadManyToDatabase(It.IsAny<IEnumerable<Tag>>()));

            this.serverMock.Setup(x => x.MapPath(It.IsAny<string>())).Returns((string s) => s);

            var fileMock = new Mock<HttpPostedFileBase>();

            // Act
            this.controller.Manage(fileMock.Object, passedTags);

            // Assert
            this.uploadTagServiceMock.Verify(x => x.UploadManyToDatabase(It.IsAny<IEnumerable<Tag>>()), Times.Once);
        }

        [Test]
        public void InvokeMapPathMethod_Once_WhenPassedParametersAreValid()
        {
            // Arrange
            string validTags = "tags";

            this.serverMock.Setup(x => x.MapPath(It.IsAny<string>())).Returns((string s) => s);

            var fileMock = new Mock<HttpPostedFileBase>();

            // Act
            this.controller.Manage(fileMock.Object, validTags);

            // Assert
            this.serverMock.Verify(x => x.MapPath(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void InvokeUploadPhotosMethod_Once_WhenPassedFileIsNotNull()
        {
            // Arrange
            this.serverMock.Setup(x => x.MapPath(It.IsAny<string>())).Returns((string s) => s);

            var fileMock = new Mock<HttpPostedFileBase>();
            this.photoUploaderMock.Setup(x => x.UploadPhotos(fileMock.Object, It.IsAny<string>(), It.IsAny<ICollection<Tag>>()));

            // Act
            this.controller.Manage(fileMock.Object, It.IsAny<string>());

            // Assert
            this.photoUploaderMock.Verify(x => x.UploadPhotos(
                fileMock.Object,
                It.IsAny<string>(),
                It.IsAny<ICollection<Tag>>()), Times.Once);
        }

        [Test]
        public void SetTempDataSuccessKey_WithCorrectValue_WhenPassedFileIsNotNull()
        {
            // Arrange
            string key = "Success";
            string value = "Upload successful";

            var fileMock = new Mock<HttpPostedFileBase>();

            // Act
            ActionResult result = this.controller.Manage(fileMock.Object, It.IsAny<string>());

            // Assert
            Assert.That(this.controller.TempData.ContainsKey(key));
            Assert.That(this.controller.TempData.ContainsValue(value));
        }
    }
}
