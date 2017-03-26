using Moq;
using NUnit.Framework;
using System;
using Visions.Auth.Contracts;
using Visions.Helpers.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Web.Areas.User.Controllers;
using Visions.Web.Common.Contracts;
using Visions.Web.Models;
using System.Web;

namespace Visions.Tests.Visions.Web.Areas.User.Controllers.ProfileControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
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
        }

        [Test]
        public void ThrowArgumentNullException_WhenServerIsNotProvided()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(
                null,
                this.uploadPhotoServiceMock.Object,
                this.uploadTagServiceMock.Object,
                this.photoServiceMock.Object,
                this.photoUploaderMock.Object,
                this.tagsHelperMock.Object,
                this.userProviderMock.Object,
                this.pagingProviderMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUploadPhotoServiceIsNotProvided()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(
                this.serverMock.Object,
                null,
                this.uploadTagServiceMock.Object,
                this.photoServiceMock.Object,
                this.photoUploaderMock.Object,
                this.tagsHelperMock.Object,
                this.userProviderMock.Object,
                this.pagingProviderMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUploadTagServiceIsNotProvided()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(
                this.serverMock.Object,
                this.uploadPhotoServiceMock.Object,
                null,
                this.photoServiceMock.Object,
                this.photoUploaderMock.Object,
                this.tagsHelperMock.Object,
                this.userProviderMock.Object,
                this.pagingProviderMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPhotoServiceIsNotProvided()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(
                this.serverMock.Object,
                this.uploadPhotoServiceMock.Object,
                this.uploadTagServiceMock.Object,
                null,
                this.photoUploaderMock.Object,
                this.tagsHelperMock.Object,
                this.userProviderMock.Object,
                this.pagingProviderMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPhotoUploaderIsNotProvided()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(
                this.serverMock.Object,
                this.uploadPhotoServiceMock.Object,
                this.uploadTagServiceMock.Object,
                this.photoServiceMock.Object,
                null,
                this.tagsHelperMock.Object,
                this.userProviderMock.Object,
                this.pagingProviderMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenTagsHelperIsNotProvided()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(
                this.serverMock.Object,
                this.uploadPhotoServiceMock.Object,
                this.uploadTagServiceMock.Object,
                this.photoServiceMock.Object,
                this.photoUploaderMock.Object,
                null,
                this.userProviderMock.Object,
                this.pagingProviderMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUserProviderIsNotProvided()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(
                this.serverMock.Object,
                this.uploadPhotoServiceMock.Object,
                this.uploadTagServiceMock.Object,
                this.photoServiceMock.Object,
                this.photoUploaderMock.Object,
                this.tagsHelperMock.Object,
                null,
                this.pagingProviderMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPagingProviderIsNotProvided()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(
                this.serverMock.Object,
                this.uploadPhotoServiceMock.Object,
                this.uploadTagServiceMock.Object,
                this.photoServiceMock.Object,
                this.photoUploaderMock.Object,
                this.tagsHelperMock.Object,
                this.userProviderMock.Object,
                null));
        }

        [Test]
        public void CreateAnInstanceOfProfileController_WhenAllParametersArePassed()
        {
            // Arrange, Act, Assert
            Assert.IsInstanceOf<ProfileController>(
                new ProfileController(
                this.serverMock.Object,
                this.uploadPhotoServiceMock.Object,
                this.uploadTagServiceMock.Object,
                this.photoServiceMock.Object,
                this.photoUploaderMock.Object,
                this.tagsHelperMock.Object,
                this.userProviderMock.Object,
                this.pagingProviderMock.Object));
        }
    }
}
