using Moq;
using NUnit.Framework;
using System;
using Visions.Services.Contracts;
using Visions.Web.Common.Contracts;
using Visions.Web.Controllers;
using Visions.Web.Models;

namespace Visions.Tests.Visions.Web.Controllers.DashboardControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPhotoServiceIsNotProvided()
        {
            // Arrange
            var pagingProviderMock = new Mock<IPagingProvider<PhotoViewModel>>();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => new DashboardController(
                null,
                pagingProviderMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPagingProviderIsNotProvided()
        {
            // Arrange
            var photoServiceMock = new Mock<IPhotoService>();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => new DashboardController(
                photoServiceMock.Object,
                null));
        }

        [Test]
        public void CreateAnInstanceOfDashboardController_WhenAllParametersAreProvided()
        {
            // Arrange
            var photoServiceMock = new Mock<IPhotoService>();
            var pagingProviderMock = new Mock<IPagingProvider<PhotoViewModel>>();

            // Act, Assert
            Assert.IsInstanceOf<DashboardController>(new DashboardController(
                photoServiceMock.Object,
                pagingProviderMock.Object));
        }
    }
}
