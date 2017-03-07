using NUnit.Framework;
using Visions.Data;

namespace Visions.Tests.Visions.Data.VisionsDbContextTests
{
    [TestFixture]
    public class Create_Should
    {
        [Test]
        public void CreateAnInstanceOfVisionsDbContext()
        {
            // Arrange, Act
            var actualResult = VisionsDbContext.Create();

            // Assert
            Assert.IsInstanceOf<VisionsDbContext>(actualResult);
        }
    }
}
