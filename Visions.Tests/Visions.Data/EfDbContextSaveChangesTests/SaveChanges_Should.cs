using Moq;
using NUnit.Framework;
using Visions.Data;
using Visions.Data.Contracts;

namespace Visions.Tests.Visions.Data.EfDbContextSaveChangesTests
{
    [TestFixture]
    public class SaveChanges_Should
    {
        [Test]
        public void InvokeSaveChangesMethodFromDbContext_Once()
        {
            // Arrange
            var dbContextMock = new Mock<IEfDbContext>();

            IEfDbContextSaveChanges dbContextSaveChanges = new EfDbContextSaveChanges(dbContextMock.Object);

            // Act
            dbContextSaveChanges.SaveChanges();

            // Assert
            dbContextMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
