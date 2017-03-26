using Moq;
using NUnit.Framework;
using System;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services;

namespace Visions.Tests.Visions.Services.ModifyServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenARepositoryIsNotProvided()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new ModifyService<Tag>(null));
        }

        [Test]
        public void CreateAnInstanceOfModifyService_WhenARepositoryIsProvided()
        {
            // Arrange
            var repositoryMock = new Mock<IEfRepository<Article>>();

            // Act, Assert
            Assert.IsInstanceOf<ModifyService<Article>>(new ModifyService<Article>(repositoryMock.Object));
        }
    }
}
