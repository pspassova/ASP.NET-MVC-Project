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
            IQueryable<User> data = new List<User>(itemsCount).AsQueryable();
            var dbSetMock = new Mock<DbSet<User>>();
            var contextMock = new Mock<IVisionsDbContext>();

            contextMock.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);
            dbSetMock.As<IQueryable<User>>().Setup(x => x.Provider).Returns(data.Provider);
            dbSetMock.As<IQueryable<User>>().Setup(x => x.Expression).Returns(data.Expression);
            dbSetMock.As<IQueryable<User>>().Setup(x => x.ElementType).Returns(data.ElementType);
            dbSetMock.As<IQueryable<User>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            IEfRepository<User> repository = new EfRepository<User>(contextMock.Object);

            // Act
            IEnumerable<User> returnedCollection = repository.GetAll();

            // Assert
            Assert.That(returnedCollection.Count, Is.EqualTo(itemsCount));
        }

        [TestCase(2)]
        public void ReturnCorrectCountOfItems_IfThereAreAny(int itemsCount)
        {
            // Arrange
            IQueryable<User> data = new List<User>(itemsCount)
            {
                new User(),
                new User()
            }.AsQueryable();
            var dbSetMock = new Mock<DbSet<User>>();
            var contextMock = new Mock<IVisionsDbContext>();

            contextMock.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);
            dbSetMock.As<IQueryable<User>>().Setup(x => x.Provider).Returns(data.Provider);
            dbSetMock.As<IQueryable<User>>().Setup(x => x.Expression).Returns(data.Expression);
            dbSetMock.As<IQueryable<User>>().Setup(x => x.ElementType).Returns(data.ElementType);
            dbSetMock.As<IQueryable<User>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            IEfRepository<User> repository = new EfRepository<User>(contextMock.Object);

            // Act
            IEnumerable<User> returnedCollection = repository.GetAll();

            // Assert
            Assert.That(returnedCollection.Count, Is.EqualTo(itemsCount));
        }
    }
}
