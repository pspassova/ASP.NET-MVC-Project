using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Visions.Helpers;
using Visions.Helpers.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;

namespace Visions.Tests.Visions.Helpers.TagHelperTests
{
    [TestFixture]
    public class CreateTags_Should
    {
        [Test]
        public void ReturnACollectionOfTags()
        {
            // Arrange
            var tagServiceMock = new Mock<ITagService>();

            ITagsHelper tagsHelper = new TagsHelper(tagServiceMock.Object);

            // Act
            ICollection<Tag> result = tagsHelper.CreateTags(null);

            // Assert
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(Tag));
        }
    }
}
