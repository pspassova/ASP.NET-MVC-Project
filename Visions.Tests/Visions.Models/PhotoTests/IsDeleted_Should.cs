using NUnit.Framework;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Models.PhotoTests
{
    [TestFixture]
    public class IsDeleted_Should
    {
        private const string PropertyName = "IsDeleted";

        [Test]
        public void BeAProperty_InPhotoClass()
        {
            // Arrange
            Photo photo = new Photo();

            // Act
            var actualResult = photo.GetType().GetProperty(PropertyName).Name;

            // Assert
            Assert.AreEqual(PropertyName, actualResult);
        }
    }
}
