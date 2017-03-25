using Moq;
using NUnit.Framework;
using PagedList;
using System.Collections.Generic;
using System.Web;
using TestStack.FluentMVCTesting;
using Visions.Auth.Contracts;
using Visions.Helpers.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Web.Areas.User.Controllers;
using Visions.Web.Common.Contracts;
using Visions.Web.Models;

namespace Visions.Tests.Visions.Web.Areas.Admin.User.Controllers.ProfileControllerTests
{
    public class Sort_Should
    {
        private ProfileController controller;
        private Mock<HttpServerUtilityBase> serverMock;
        private Mock<IUploadService<Photo>> uploadPhotoServiceMock;
        private Mock<IUploadService<Tag>> uploadTagServiceMock;
        private Mock<IPhotoService> photoServiceMock;
        private Mock<IPhotoUploader> photoUploaderMock;
        private Mock<ITagsHelper> tagsConvertHelperMock;
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
            this.tagsConvertHelperMock = new Mock<ITagsHelper>();
            this.userProviderMock = new Mock<IUserProvider>();
            this.pagingProviderMock = new Mock<IPagingProvider<PhotoViewModel>>();

            this.controller = new ProfileController(
                this.serverMock.Object,
                this.uploadPhotoServiceMock.Object,
                this.uploadTagServiceMock.Object,
                this.photoServiceMock.Object,
                this.photoUploaderMock.Object,
                this.tagsConvertHelperMock.Object,
                this.userProviderMock.Object,
                this.pagingProviderMock.Object);
        }

        [Test]
        public void SetViewBagKey_WithTheCorrectValue_WhenParametersAreValid()
        {
            // Arrange
            string expectedValue = "text";
            int pageNumber = 1;
            int pageSize = 4;

            // Act
            this.controller.Sort(expectedValue, pageNumber, pageSize);

            // Assert
            Assert.AreEqual(expectedValue, this.controller.ViewBag.SelectedTag);
        }

        [Test]
        public void InvokeGetUserId_Once_WhenPassedParametersAreValid()
        {
            // Arrange
            string sortByText = "text";
            int pageNumber = 1;
            int pageSize = 4;

            //  Act
            this.controller.Sort(sortByText, pageNumber, pageSize);

            // Assert
            this.userProviderMock.Verify(x => x.GetUserId(), Times.Once);
        }

        [Test]
        public void InvokeSortByTagMethod_Once_WhenPassedParametersAreValid()
        {
            // Arrange
            string sortByText = "text";
            int pageNumber = 1;
            int pageSize = 4;

            string userId = "id";
            this.userProviderMock.Setup(x => x.GetUserId()).Returns(userId);

            // Act
            this.controller.Sort(sortByText, pageNumber, pageSize);

            // Assert
            this.photoServiceMock.Verify(x => x.SortByTag(sortByText, userId), Times.Once);
        }

        [Test]
        public void InvokeCreatePagedList_Once_WhenPassedParametersAreValid()
        {
            // Arrange
            string sortByText = "text";
            int pageNumber = 1;
            int pageSize = 4;

            var photoViewModelMock = new Mock<IPagedList<PhotoViewModel>>();
            this.pagingProviderMock.Setup(x => x.CreatePagedList(It.IsAny<IEnumerable<PhotoViewModel>>(), pageNumber, pageSize))
                .Returns(photoViewModelMock.Object);

            // Act
            this.controller.Sort(sortByText, pageNumber, pageSize);

            // Assert
            this.pagingProviderMock.Verify(x => x.CreatePagedList(It.IsAny<IEnumerable<PhotoViewModel>>(), pageNumber, pageSize), Times.Once);
        }

        [Test]
        public void RendeDashboardView_WhenPassedParametersAreValid()
        {
            // Arrange
            string viewName = "Dashboard";
            string sortByText = "text";
            int pageNumber = 1;
            int pageSize = 4;

            // Act, Assert
            this.controller.WithCallTo(x => x.Sort(sortByText, pageNumber, pageSize))
                .ShouldRenderView(viewName);
        }

        [Test]
        public void RenderDashboardView_WithTheCorrectModel_WhenPassedParametersAreValid()
        {
            // Arrange
            string viewName = "Dashboard";
            string sortByText = "text";
            int pageNumber = 1;
            int pageSize = 4;

            var viewModelMock = new Mock<IPagedList<PhotoViewModel>>();
            this.pagingProviderMock.Setup(x => x.CreatePagedList(It.IsAny<IEnumerable<PhotoViewModel>>(), pageNumber, pageSize))
                .Returns(viewModelMock.Object);

            // Act, Assert
            this.controller.WithCallTo(x => x.Sort(sortByText, pageNumber, pageSize))
                .ShouldRenderView(viewName)
                .WithModel(viewModelMock.Object);
        }
    }
}
