using Moq;
using NUnit.Framework;
using PagedList;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;
using Visions.Services.Contracts;
using Visions.Web.Common.Contracts;
using Visions.Web.Controllers;
using Visions.Web.Models;

namespace Visions.Tests.Visions.Web.Controllers.DashboardControllerTests
{
    [TestFixture]
    public class Shared_Should
    {
        private DashboardController controller;
        private Mock<IPhotoService> photoServiceMock;
        private Mock<IPagingProvider<PhotoViewModel>> pagingProviderMock;

        [SetUp]
        public void Setup()
        {
            this.photoServiceMock = new Mock<IPhotoService>();
            this.pagingProviderMock = new Mock<IPagingProvider<PhotoViewModel>>();

            this.controller = new DashboardController(photoServiceMock.Object, pagingProviderMock.Object);
        }

        [Test]
        public void SetViewBagKey_WithTheCorrectValue_WhenParametersAreValid()
        {
            // Arrange
            string expectedValue = "text";
            int pageNumber = 1;
            int pageSize = 4;

            // Act
            this.controller.Shared(expectedValue, pageNumber, pageSize);

            // Assert
            Assert.AreEqual(expectedValue, this.controller.ViewBag.SelectedTag);
        }

        [Test]
        public void InvokeSortByTagMethod_Once_WhenPassedParametersAreValid()
        {
            // Arrange
            string sortByText = "text";
            int pageNumber = 1;
            int pageSize = 4;

            // Act
            this.controller.Shared(sortByText, pageNumber, pageSize);

            // Assert
            this.photoServiceMock.Verify(x => x.SortByTag(sortByText, It.IsAny<string>()), Times.Once);
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
            this.controller.Shared(sortByText, pageNumber, pageSize);

            // Assert
            this.pagingProviderMock.Verify(x => x.CreatePagedList(It.IsAny<IEnumerable<PhotoViewModel>>(), pageNumber, pageSize), Times.Once);
        }

        [Test]
        public void RenderSharedView_WhenPassedParametersAreValid()
        {
            // Arrange
            string viewName = "Shared";
            string sortByText = "text";
            int pageNumber = 1;
            int pageSize = 4;

            // Act, Assert
            this.controller.WithCallTo(x => x.Shared(sortByText, pageNumber, pageSize))
                .ShouldRenderView(viewName);
        }

        [Test]
        public void RenderSharedView_WithTheCorrectModel_WhenPassedParametersAreValid()
        {
            // Arrange
            string viewName = "Shared";
            string sortByText = "text";
            int pageNumber = 1;
            int pageSize = 4;

            var viewModelMock = new Mock<IPagedList<PhotoViewModel>>();
            this.pagingProviderMock.Setup(x => x.CreatePagedList(It.IsAny<IEnumerable<PhotoViewModel>>(), pageNumber, pageSize))
                .Returns(viewModelMock.Object);

            // Act, Assert
            this.controller.WithCallTo(x => x.Shared(sortByText, pageNumber, pageSize))
                .ShouldRenderView(viewName)
                .WithModel(viewModelMock.Object);
        }
    }
}
