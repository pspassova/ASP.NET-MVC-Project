using Moq;
using NUnit.Framework;
using Visions.Models.Models;
using Visions.Services;
using Visions.Services.Contracts;

namespace Visions.Tests.Visions.Services.TagServiceTests
{
    [TestFixture]
    public class Create_Should
    {
        [Test]
        public void ReturnAnInstanceOfTag()
        {
            // Arrange
            ITagService tagService = new TagService();

            // Act, Assert
            Assert.IsInstanceOf<Tag>(tagService.Create(It.IsAny<string>()));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("test text")]
        public void SetTheCorrectTextToTextProperty_WhenReturningTheNewTag(string tagText)
        {
            // Arrange
            ITagService tagService = new TagService();

            // Act
            Tag tag = tagService.Create(tagText);

            // Assert
            Assert.AreEqual(tagText, tag.Text);
        }
    }
}
