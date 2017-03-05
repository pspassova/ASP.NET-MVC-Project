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
            var mockedContext = new Mock<IVisionsDbContext>();
            UnitOfWork unitOfWork = new UnitOfWork(mockedContext.Object);

            // Act
            unitOfWork.Commit();

            // Assert
            mockedContext.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
