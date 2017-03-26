using Moq;
using NUnit.Framework;
using System;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services;

namespace Visions.Tests.Visions.Services.DeleteServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenARepositoryIsNotProvided()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new DeleteService<Tag>(null));
        }

        [Test]
        public void CreateAnInstanceOfDeleteService_WhenARepositoryIsProvided()
        {
            // Arrange
            var repositoryMock = new Mock<IEfRepository<Article>>();

            // Act, Assert
            Assert.IsInstanceOf<DeleteService<Article>>(new DeleteService<Article>(repositoryMock.Object));
        }
    }
}
