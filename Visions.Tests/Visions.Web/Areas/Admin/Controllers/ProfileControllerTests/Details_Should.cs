using Moq;
using NUnit.Framework;
using System;
using System.Web.Mvc;
using Visions.Helpers.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Web.Areas.Admin.Controllers;
using Visions.Web.Helpers.Contracts;

namespace Visions.Tests.Visions.Web.Areas.Admin.Controllers.ProfileControllerTests
{
    [TestFixture]
    class Details_Should
    {
        private ProfileController controller;

        [SetUp]
        public void Setup()
        {
            var uploadTagServiceMock = new Mock<IUploadService<Tag>>();
            var modifyPhotoServiceMock = new Mock<IModifyService<Photo>>();
            var deletePhotoServiceMock = new Mock<IDeleteService<Photo>>();
            var photoServiceMock = new Mock<IPhotoService>();
            var photoUploaderMock = new Mock<IPhotoUploader>();
            var tagsHelperMock = new Mock<ITagsHelper>();

            this.controller = new ProfileController(
                uploadTagServiceMock.Object,
                modifyPhotoServiceMock.Object,
                deletePhotoServiceMock.Object,
                photoServiceMock.Object,
                photoUploaderMock.Object,
                tagsHelperMock.Object);


        }

        [Test]
        public void ReturnHttpNotFoundResult_WhenPassedIdIsNotFound()
        {
            // Arrange, Act
            var result = this.controller.Details(Guid.NewGuid());

            // Assert
            Assert.IsInstanceOf<HttpNotFoundResult>(result);
        }
    }
}
