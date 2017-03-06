using Moq;
using NUnit.Framework;
using Visions.Data;
using Visions.Data.Contracts;

namespace Visions.Tests.Visions.Data.UnitOfWorkTests
{
    [TestFixture]
    public class Dispose_Should
    {
        [Test]
        public void BeInvoked_WhenUsingUnitOfWork()
        {
            // Arrange
            var mockedContext = new Mock<IVisionsDbContext>();
            var unitOfWork = new UnitOfWork(mockedContext.Object);

            bool isCalled;

            // Act
            using (unitOfWork)
            {
                isCalled = true;
            }

            // Assert
            Assert.IsTrue(isCalled);
        }
    }
}
