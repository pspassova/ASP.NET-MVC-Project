﻿using Moq;
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
        public void ThrowArgumentNullException_WhenARepositoryIsNotProvided()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new UploadService<Tag>(null));
        }

        [Test]
        public void CreateAnInstanceOfUploadService_WhenARepositoryIsProvided()
        {
            // Arrange
            var repositoryMock = new Mock<IEfRepository<Photo>>();

            // Act, Assert
            Assert.IsInstanceOf<UploadService<Photo>>(new UploadService<Photo>(repositoryMock.Object));
        }
    }
}
