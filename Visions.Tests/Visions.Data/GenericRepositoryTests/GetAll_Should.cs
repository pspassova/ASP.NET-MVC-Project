using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Visions.Data;
using Visions.Data.Contracts;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Data.GenericRepositoryTests
{
    [TestFixture]
    public class GetAll_Should
    {
        [TestCase(0)]
        public void ReturnCorrectCountOfItems_IfThereAreNone(int itemsCount)
        {
            // Arrange
            var dbSetMock = new Mock<DbSet<User>>();
            IEnumerable<User> data = new List<User>(itemsCount);
            dbSetMock.As<IDbSet<User>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var contextMock = new Mock<IVisionsDbContext>();
            contextMock.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);

            IGenericRepository<User> repository = new GenericRepository<User>(contextMock.Object);

            // Act
            IEnumerable<User> returnedCollection = repository.GetAll();

            // Assert
            Assert.That(returnedCollection.Count, Is.EqualTo(itemsCount));
        }

        [TestCase(2)]
        public void ReturnCorrectCountOfItems_IfThereAreAny(int itemsCount)
        {
            // Arrange
            var dbSetMock = new Mock<DbSet<User>>();
            IEnumerable<User> data = new List<User>(itemsCount)
            {
                new Mock<User>().Object,
                new Mock<User>().Object
            };
            dbSetMock.As<IDbSet<User>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var contextMock = new Mock<IVisionsDbContext>();
            contextMock.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);

            IGenericRepository<User> repository = new GenericRepository<User>(contextMock.Object);

            // Act
            IEnumerable<User> returnedCollection = repository.GetAll();

            // Assert
            Assert.That(returnedCollection.Count, Is.EqualTo(itemsCount));
        }
    }
}
