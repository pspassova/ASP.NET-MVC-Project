using NUnit.Framework;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Models.UserTests
{
    [TestFixture]
    public class AvatarPath_Should
    {
        private const string PropertyName = "AvatarPath";

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
