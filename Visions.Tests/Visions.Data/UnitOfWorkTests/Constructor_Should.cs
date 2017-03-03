using NUnit.Framework;
using System;
using Visions.Data;

namespace Visions.Tests.Visions.Data.UnitOfWorkTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenIVisionsDbContextIsNull()
        {
            // Arrange, Act
            var exception = Assert.Throws<ArgumentNullException>(() => new UnitOfWork(null));

            // Assert
            StringAssert.IsMatch("context", exception.ParamName);
        }
    }
}
