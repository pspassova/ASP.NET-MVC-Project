using Moq;
using NUnit.Framework;
using System;
using Visions.Helpers;
using Visions.Services.Contracts;

namespace Visions.Tests.Visions.Helpers.TagHelperTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenTagServiceIsNull()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new TagsHelper(null));
        }

        [Test]
        public void CreateAnInstanceOfTagsHelper_WhenTagServiceIsProvided()
        {
            // Arrange
            var tagServiceMock = new Mock<ITagService>();

            // Act, Assert
            Assert.IsInstanceOf<TagsHelper>(new TagsHelper(tagServiceMock.Object));
        }
    }
}
