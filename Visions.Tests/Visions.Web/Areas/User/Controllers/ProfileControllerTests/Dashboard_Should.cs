using Moq;
using NUnit.Framework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Visions.Auth.Contracts;
using Visions.Helpers.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Services.Enumerations;
using Visions.Web.Areas.User.Controllers;
using Visions.Web.Common.Contracts;
using Visions.Web.Models;
using TestStack.FluentMVCTesting;
using System.Web;
using System.Web.Mvc;

namespace Visions.Tests.Visions.Web.Areas.User.Controllers.ProfileControllerTests
{
    [TestFixture]
    public class Dashboard_Should
    {
        private ProfileController controller;
        private Mock<HttpServerUtilityBase> serverMock;
        private Mock<IUploadService<Photo>> uploadPhotoServiceMock;
        private Mock<IUploadService<Tag>> uploadTagServiceMock;
        private Mock<IPhotoService> photoServiceMock;
        private Mock<IPhotoUploader> photoUploaderMock;
        private Mock<ITagsHelper> tagsHelperMock;
        private Mock<IUserProvider> userProviderMock;
        private Mock<IPagingProvider<PhotoViewModel>> pagingProviderMock;

        [SetUp]
        public void Setup()
        {
            this.serverMock = new Mock<HttpServerUtilityBase>();
            this.uploadPhotoServiceMock = new Mock<IUploadService<Photo>>();
            this.uploadTagServiceMock = new Mock<IUploadService<Tag>>();
            this.photoServiceMock = new Mock<IPhotoService>();
            this.photoUploaderMock = new Mock<IPhotoUploader>();
            this.tagsHelperMock = new Mock<ITagsHelper>();
            this.userProviderMock = new Mock<IUserProvider>();
            this.pagingProviderMock = new Mock<IPagingProvider<PhotoViewModel>>();

            this.controller = new ProfileController(
                this.serverMock.Object,
                this.uploadPhotoServiceMock.Object,
                this.uploadTagServiceMock.Object,
                this.photoServiceMock.Object,
                this.photoUploaderMock.Object,
                this.tagsHelperMock.Object,
                this.userProviderMock.Object,
                this.pagingProviderMock.Object);
        }

        [TestCase(1, 4)]
        [TestCase(5, 23)]
        public void InvokeGetUserId_Once_WhenPassedParametersAreValid(int pageNumber, int pageSize)
        {
            // Arrange, Act
            this.controller.Dashboard(pageNumber, pageSize);

            // Assert
            this.userProviderMock.Verify(x => x.GetUserId(), Times.Once);
        }

        //[Test]
        //public void InvokeGetAllMethod_Once_WhenPassedParametersAreValid()
        //{
        //    // Arrange
        //    string userId = "testId";
        //    this.userProviderMock.Setup(x => x.GetUserId()).Returns(userId);

        //    Expression<Func<Photo, DateTime?>> createdOn = (Photo photo) => photo.CreatedOn;
        //    OrderBy orderBy = OrderBy.Descending;
        //    Expression<Func<Photo, PhotoViewModel>> selectAs = (Photo photo) => new PhotoViewModel();

        //    var photosMock = new Mock<IEnumerable<PhotoViewModel>>();
        //    this.photoServiceMock.Setup(x => x.GetAll(userId, createdOn, orderBy, selectAs))
        //        .Returns(photosMock.Object).Verifiable();

        //    int pageNumber = 1;
        //    int pageSize = 4;

        //    // Act
        //    this.controller.Dashboard(pageNumber, pageSize);

        //    // Assert
        //    this.photoServiceMock.Verify(x => x.GetAll(userId, createdOn, orderBy, selectAs), Times.Once);
        //}

        [Test]
        public void InvokeCreatePagedList_Once_WhenPassedParametersAreValid()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 4;

            var photoViewModelMock = new Mock<IPagedList<PhotoViewModel>>();
            this.pagingProviderMock.Setup(x => x.CreatePagedList(It.IsAny<IEnumerable<PhotoViewModel>>(), pageNumber, pageSize))
                .Returns(photoViewModelMock.Object);

            // Act
            this.controller.Dashboard(pageNumber, pageSize);

            // Assert
            this.pagingProviderMock.Verify(x => x.CreatePagedList(It.IsAny<IEnumerable<PhotoViewModel>>(), pageNumber, pageSize), Times.Once);
        }

        [Test]
        public void RenderDefaultView_WhenPassedParametersAreValid()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 4;

            // Act, Assert
            this.controller.WithCallTo(x => x.Dashboard(pageNumber, pageSize))
                .ShouldRenderDefaultView();
        }

        [Test]
        public void RenderDefaultView_WithTheCorrectModel_WhenPassedParametersAreValid()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 4;

            var viewModelMock = new Mock<IPagedList<PhotoViewModel>>();
            this.pagingProviderMock.Setup(x => x.CreatePagedList(It.IsAny<IEnumerable<PhotoViewModel>>(), pageNumber, pageSize))
                .Returns(viewModelMock.Object);

            // Act, Assert
            this.controller.WithCallTo(x => x.Dashboard(pageNumber, pageSize))
                .ShouldRenderDefaultView()
                .WithModel(viewModelMock.Object);
        }

        [Test]
        public void RedirectToTheCorrectAction_WhenPassedFileIsNull()
        {
            // Arrange 
            int pageNumber = 1;
            int pageSize = 4;

            // Act
            this.controller.WithCallTo(x => x.Dashboard(null, It.IsAny<string>()))
                .ShouldRedirectTo(c => c.Dashboard(pageNumber, pageSize));
        }

        [Test]
        public void RedirectToTheCorrectAction_WhenPassedFileIsNotNull()
        {
            // Arrange 
            int pageNumber = 1;
            int pageSize = 4;

            var fileMock = new Mock<HttpPostedFileBase>();

            // Act
            this.controller.WithCallTo(x => x.Dashboard(fileMock.Object, It.IsAny<string>()))
                .ShouldRedirectTo(c => c.Dashboard(pageNumber, pageSize));
        }

        [Test]
        public void SetTempDataSuccessKey_WithCorrectValue_WhenPassedFileIsNull()
        {
            // Arrange
            string key = "Success";
            string value = "Upload failed";

            // Act
            ActionResult result = this.controller.Dashboard(null, It.IsAny<string>());

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
            this.controller.Dashboard(fileMock.Object, passedTags);

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
            this.controller.Dashboard(fileMock.Object, passedTags);

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
            this.controller.Dashboard(fileMock.Object, validTags);

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
            this.controller.Dashboard(fileMock.Object, It.IsAny<string>());

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
            ActionResult result = this.controller.Dashboard(fileMock.Object, It.IsAny<string>());

            // Assert
            Assert.That(this.controller.TempData.ContainsKey(key));
            Assert.That(this.controller.TempData.ContainsValue(value));
        }
    }
}
