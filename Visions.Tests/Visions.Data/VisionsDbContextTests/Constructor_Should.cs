using NUnit.Framework;
using System.Data.Entity;
using Visions.Data;
using Visions.Data.Contracts;

namespace Visions.Tests.Visions.Data.VisionsDbContextTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateAnInstanceOfDbContext()
        {
            // Arrange
            IVisionsDbContext context = new VisionsDbContext();

            // Act, Assert
            Assert.That(context, Is.InstanceOf<DbContext>());
        }

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
