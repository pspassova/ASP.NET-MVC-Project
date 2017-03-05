﻿using Moq;
using NUnit.Framework;
using System;
using System.Data.Entity;
using Visions.Data;
using Visions.Data.Contracts;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Data.GenericRepositoryTests
{
    [TestFixture]
    public class GetById_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenEntityIsNull()
        {
            // Arange
            var dbSetMock = new Mock<DbSet<User>>();
            var contextMock = new Mock<IVisionsDbContext>();
            contextMock.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);

            IGenericRepository<User> repository = new GenericRepository<User>(contextMock.Object);

            // Arrange, Act
            var exception = Assert.Throws<ArgumentNullException>(() => repository.GetById(null));

            // Assert
            StringAssert.IsMatch("id", exception.ParamName);
        }
    }
}
