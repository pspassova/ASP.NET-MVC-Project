using Moq;
using NUnit.Framework;
using System;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services;

namespace Visions.Tests.Visions.Services.UploadServicetests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenADbSetWrapperIsNotProvided()
        {
            // Arrange
            var dbContextSaveChangesMock = new Mock<IEfDbContextSaveChanges>();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => new UploadService<Tag>(null, dbContextSaveChangesMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenADbContextSaveChangesIsNotProvided()
        {
            // Arrange
            var dbSetWrapperMock = new Mock<IEfDbSetWrapper<Photo>>();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => new UploadService<Photo>(dbSetWrapperMock.Object, null));
        }

        [Test]
        public void CreateAnInstanceOfUploadService_BothParametersAreProvided()
        {
            // Arrange
            var dbSetWrapperMock = new Mock<IEfDbSetWrapper<Photo>>();
            var dbContextSaveChangesMock = new Mock<IEfDbContextSaveChanges>();

            // Act, Assert
            Assert.IsInstanceOf<UploadService<Photo>>(new UploadService<Photo>(dbSetWrapperMock.Object, dbContextSaveChangesMock.Object));
        }
    }
}
