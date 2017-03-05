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
            IVisionsDbContext context = new VisionsDbContext();

            // Act
            IDbSet<User> contextSet = context.Set<User>();

            // Assert
            Assert.That(contextSet, Is.InstanceOf<DbSet<User>>());
        }
    }
}
