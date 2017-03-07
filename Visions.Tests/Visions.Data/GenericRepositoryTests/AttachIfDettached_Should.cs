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
    public class AttachIfDettached_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenEntityIsNull()
        {
            // Arange
            var dbSetMock = new Mock<DbSet<User>>();
            var contextMock = new Mock<IVisionsDbContext>();
            contextMock.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);

            IGenericRepository<User> repository = new GenericRepository<User>(contextMock.Object);

            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => repository.AttachIfDetached(null));

            // Assert
            StringAssert.IsMatch("entity", exception.ParamName);
        }

        [Test]
        public void InvokeGetStatefulMethod_Once_WhenEntityIsProvided()
        {
            // Arange
            var dbSetMock = new Mock<DbSet<User>>();
            var contextMock = new Mock<IVisionsDbContext>();
            contextMock.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);

            var statefulMock = new Mock<IStateful<User>>();
            contextMock.Setup(x => x.GetStateful<User>(It.IsAny<User>())).Returns(statefulMock.Object);

            IGenericRepository<User> repository = new GenericRepository<User>(contextMock.Object);

            // Act
            repository.AttachIfDetached(new Mock<User>().Object);

            // Assert
            contextMock.Verify(x => x.GetStateful(It.IsAny<User>()), Times.Once());
        }

        [Test]
        public void InvokeAttachMethodFromIDbSet_Once_IfStateOfEntityIsDettached()
        {
            // Arange
            var dbSetMock = new Mock<DbSet<User>>();
            var contextMock = new Mock<IVisionsDbContext>();
            contextMock.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);

            var statefulMock = new Mock<IStateful<User>>();
            statefulMock.Setup(x => x.EntityState).Returns(EntityState.Detached);
            contextMock.Setup(x => x.GetStateful<User>(It.IsAny<User>())).Returns(statefulMock.Object);

            IGenericRepository<User> repository = new GenericRepository<User>(contextMock.Object);

            // Act
            repository.AttachIfDetached(new Mock<User>().Object);

            // Assert
            dbSetMock.Verify(x => x.Attach(It.IsAny<User>()), Times.Once());
        }

        [Test]
        public void NotInvokeAttachMethodFromIDbSet_IfStateOfEntityIsAttached()
        {
            // Arange
            var dbSetMock = new Mock<DbSet<User>>();
            var contextMock = new Mock<IVisionsDbContext>();
            contextMock.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);

            var statefulMock = new Mock<IStateful<User>>();
            contextMock.Setup(x => x.GetStateful<User>(It.IsAny<User>())).Returns(statefulMock.Object);

            IGenericRepository<User> repository = new GenericRepository<User>(contextMock.Object);

            // Act
            repository.AttachIfDetached(new Mock<User>().Object);

            // Assert
            dbSetMock.Verify(x => x.Attach(It.IsAny<User>()), Times.Never);
        }
    }
}
