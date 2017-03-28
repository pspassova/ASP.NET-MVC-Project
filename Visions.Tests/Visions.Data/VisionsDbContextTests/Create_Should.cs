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
            var actualResult = EfDbContext.Create();

            // Assert
            Assert.IsInstanceOf<EfDbContext>(actualResult);
        }
    }
}
