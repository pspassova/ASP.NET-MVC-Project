using Moq;
using NUnit.Framework;
using System;
using System.Data.Entity;
using Visions.Data;
using Visions.Data.Contracts;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Data.GenericRepositoryTests
{
    [TestFixture]
    public class Delete_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenEntityIsNull()
        {
            // Arange
            var dbSetMock = new Mock<DbSet<User>>();
            var contextMock = new Mock<IVisionsDbContext>();
            contextMock.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);

            IEfRepository<User> repository = new EfRepository<User>(contextMock.Object);

            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => repository.Delete(null));

            // Assert
            StringAssert.IsMatch("entity", exception.ParamName);
        }

        //[Test]
        //public void InvokeGetStatefulMethodFromContext_Once_WhenEntityIsProvided()
        //{
        //    // Arange
        //    var dbSetMock = new Mock<DbSet<User>>();
        //    var contextMock = new Mock<IVisionsDbContext>();
        //    contextMock.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);

        //    var statefulMock = new Mock<IStateful<User>>();
        //    contextMock.Setup(x => x.GetStateful(It.IsAny<User>())).Returns(statefulMock.Object);

        //    IEfRepository<User> repository = new EfRepository<User>(contextMock.Object);

        //    // Act
        //    repository.Delete(new Mock<User>().Object);

        //    // Assert
        //    contextMock.Verify(x => x.GetStateful(It.IsAny<User>()), Times.Once());
        //}

        //[Test]
        //public void ChangeEntityStateOfIStatefulAsDeleted_WhenEntityIsProvided()
        //{
        //    // Arange
        //    var dbSetMock = new Mock<DbSet<User>>();
        //    var contextMock = new Mock<IVisionsDbContext>();
        //    contextMock.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);

        //    var statefulMock = new Mock<IStateful<User>>();
        //    statefulMock.SetupSet(x => x.EntityState = EntityState.Deleted).Verifiable();
        //    contextMock.Setup(x => x.GetStateful(It.IsAny<User>())).Returns(statefulMock.Object);

        //    IEfRepository<User> repository = new EfRepository<User>(contextMock.Object);

        //    // Act
        //    repository.Delete(new Mock<User>().Object);

        //    // Assert
        //    statefulMock.Verify();
        //}
    }
}
