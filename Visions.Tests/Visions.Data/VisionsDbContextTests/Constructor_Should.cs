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
            IEfDbContext context = new EfDbContext();

            // Act, Assert
            Assert.IsInstanceOf<DbContext>(context);
        }

        [Test]
        public void CreateAnInstanceOfVisionsDbContext()
        {
            // Arrange
            IEfDbContext context = new EfDbContext();

            // Act, Assert
            Assert.IsInstanceOf<EfDbContext>(context);
        }
    }
}
