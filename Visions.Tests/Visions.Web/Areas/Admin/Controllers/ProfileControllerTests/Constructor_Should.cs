using Moq;
using NUnit.Framework;
using System;
using System.Web;
using Visions.Helpers.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Web.Areas.Admin.Controllers;
using Visions.Web.Common.Contracts;

namespace Visions.Tests.Visions.Web.Areas.Admin.Controllers.ProfileControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
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
        }

        [Test]
        public void ThrowArgumentNullException_WhenServerIsNotProvided()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(
                null,
                this.uploadTagServiceMock.Object,
                this.modifyPhotoServiceMock.Object,
                this.deletePhotoServiceMock.Object,
                this.photoServiceMock.Object,
                this.photoUploaderMock.Object,
                this.photoConverterMock.Object,
                this.tagsHelperMock.Object));
        }


        [Test]
        public void ThrowArgumentNullException_WhenUploadTagServiceIsNotProvided()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(
                this.serverMock.Object,
                null,
                this.modifyPhotoServiceMock.Object,
                this.deletePhotoServiceMock.Object,
                this.photoServiceMock.Object,
                this.photoUploaderMock.Object,
                this.photoConverterMock.Object,
                this.tagsHelperMock.Object));
        }


        [Test]
        public void ThrowArgumentNullException_WhenModifyPhotoServiceIsNotProvided()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(
                this.serverMock.Object,
                this.uploadTagServiceMock.Object,
                null,
                this.deletePhotoServiceMock.Object,
                this.photoServiceMock.Object,
                this.photoUploaderMock.Object,
                this.photoConverterMock.Object,
                this.tagsHelperMock.Object));
        }


        [Test]
        public void ThrowArgumentNullException_WhenDeletePhotoServiceIsNotProvided()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(
                this.serverMock.Object,
                this.uploadTagServiceMock.Object,
                this.modifyPhotoServiceMock.Object,
                null,
                this.photoServiceMock.Object,
                this.photoUploaderMock.Object,
                this.photoConverterMock.Object,
                this.tagsHelperMock.Object));
        }


        [Test]
        public void ThrowArgumentNullException_WhenPhotoServiceIsNotProvided()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(
                this.serverMock.Object,
                this.uploadTagServiceMock.Object,
                this.modifyPhotoServiceMock.Object,
                this.deletePhotoServiceMock.Object,
                null,
                this.photoUploaderMock.Object,
                this.photoConverterMock.Object,
                this.tagsHelperMock.Object));
        }


        [Test]
        public void ThrowArgumentNullException_WhenPhotoUploaderIsNotProvided()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(
                this.serverMock.Object,
                this.uploadTagServiceMock.Object,
                this.modifyPhotoServiceMock.Object,
                this.deletePhotoServiceMock.Object,
                this.photoServiceMock.Object,
                null,
                this.photoConverterMock.Object,
                this.tagsHelperMock.Object));
        }


        [Test]
        public void ThrowArgumentNullException_WhenPhotoConverterIsNotProvided()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(
                this.serverMock.Object,
                this.uploadTagServiceMock.Object,
                this.modifyPhotoServiceMock.Object,
                this.deletePhotoServiceMock.Object,
                this.photoServiceMock.Object,
                this.photoUploaderMock.Object,
                null,
                this.tagsHelperMock.Object));
        }


        [Test]
        public void ThrowArgumentNullException_WhenTagsHelperIsNotProvided()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(
                this.serverMock.Object,
                this.uploadTagServiceMock.Object,
                this.modifyPhotoServiceMock.Object,
                this.deletePhotoServiceMock.Object,
                this.photoServiceMock.Object,
                this.photoUploaderMock.Object,
                this.photoConverterMock.Object,
                null));
        }

        [Test]
        public void CreateAnInstanceOfProfileController_WhenAllParametersAreProvided()
        {
            // Arrange, Act, Assert
            Assert.IsInstanceOf<ProfileController>(
                new ProfileController(
                this.serverMock.Object,
                this.uploadTagServiceMock.Object,
                this.modifyPhotoServiceMock.Object,
                this.deletePhotoServiceMock.Object,
                this.photoServiceMock.Object,
                this.photoUploaderMock.Object,
                this.photoConverterMock.Object,
                this.tagsHelperMock.Object));
        }
    }
}
