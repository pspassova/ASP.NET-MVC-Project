using Moq;
using NUnit.Framework;
using System;
using Visions.Data;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Data.StatefulTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenDbEntityEntryIsNull()
        {
            // Arrange, Act
            var exception = Assert.Throws<ArgumentNullException>(() => new Stateful<User>(null));

            // Assert
            StringAssert.IsMatch("entry", exception.ParamName);
        }
    }
}
