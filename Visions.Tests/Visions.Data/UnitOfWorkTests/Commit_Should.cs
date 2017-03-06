using Moq;
using NUnit.Framework;
using Visions.Data;
using Visions.Data.Contracts;

namespace Visions.Tests.Visions.Data.UnitOfWorkTests
{
    public class Commit_Should
    {
        [Test]
        public void InvokeContextMethod_Once()
        {
            // Arrange
            var contextMock = new Mock<IVisionsDbContext>();
            UnitOfWork unitOfWork = new UnitOfWork(contextMock.Object);

            // Act
            unitOfWork.Commit();

            // Assert
            contextMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
