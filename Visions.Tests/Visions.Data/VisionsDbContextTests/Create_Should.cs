using NUnit.Framework;
using Visions.Data;
using Visions.Data.Contracts;

namespace Visions.Tests.Visions.Data.VisionsDbContextTests
{
    [TestFixture]
    public class Create_Should
    {
        [Test]
        public void CreateAnInstanceOfVisionsDbContext()
        {
            // Arrange
            IVisionsDbContext context = new VisionsDbContext();

            // Act, Assert
            Assert.That(context, Is.InstanceOf<VisionsDbContext>());
        }
    }
}
