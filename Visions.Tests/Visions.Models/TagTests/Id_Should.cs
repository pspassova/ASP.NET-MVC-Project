using NUnit.Framework;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Models.TagTests
{
    [TestFixture]
    public class Id_Should
    {
        [Test]
        public void BeAProperty_InTagClass()
        {
            // Arrange
            string propertyName = "Id";

            Tag tag = new Tag();

            // Act
            var actualResult = tag.GetType().GetProperty(propertyName).Name;

            // Assert
            Assert.AreEqual(propertyName, actualResult);
        }
    }
}
