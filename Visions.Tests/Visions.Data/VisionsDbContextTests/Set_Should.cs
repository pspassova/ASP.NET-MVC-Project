using NUnit.Framework;
using System.Data.Entity;
using Visions.Data;
using Visions.Data.Contracts;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Data.VisionsDbContextTests
{
    [TestFixture]
    public class Set_Should
    {
        [Test]
        public void ReturnInstanceOfDbBaseSetOfTheCorrectClass_WhenAClassIsProvided()
        {
            // Arrange
            IEfDbContext context = new EfDbContext();

            // Act
            var actualResult = context.Set<User>();

            // Assert
            Assert.IsInstanceOf<DbSet<User>>(actualResult);
        }
    }
}
