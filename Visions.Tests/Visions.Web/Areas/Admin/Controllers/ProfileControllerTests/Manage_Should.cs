using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;
using Visions.Helpers.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Web.Areas.Admin.Controllers;
using Visions.Web.Helpers.Contracts;
using Visions.Web.Models;

namespace Visions.Tests.Visions.Web.Areas.Admin.Controllers.ProfileControllerTests
{
    [TestFixture]
    public class Manage_Should
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
    }
}
