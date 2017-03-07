using NUnit.Framework;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Models.UserTests
{
    [TestFixture]
    public class PinnedPhotos_Should
    {
        private const string PropertyName = "PinnedPhotos";

        [Test]
        public void BeAProperty_InPhotoClass()
        {
            // Arrange
            User user = new User();

            // Act
            var actualResult = user.GetType().GetProperty(PropertyName).Name;

            // Assert
            Assert.AreEqual(PropertyName, actualResult);
        }
    }
}
