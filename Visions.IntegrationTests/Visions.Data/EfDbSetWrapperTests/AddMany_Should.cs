using Ninject;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Web.App_Start;

namespace Visions.IntegrationTests.Visions.Data.EfDbSetWrapperTests
{
    [TestFixture]
    public class AddMany_Should
    {
        private static IKernel kernel;

        [SetUp]
        public void Setup()
        {
            kernel = NinjectWebCommon.CreateKernel();
        }

        [Test]
        public void ChangeEntriesEntityStateToAdded_WhenEntitiesAreProvided()
        {
            // Arrange
            IEfDbSetWrapper<Photo> dbSetWrapper = kernel.Get<IEfDbSetWrapper<Photo>>();

            IEnumerable<Photo> entities = new List<Photo>()
            {
                new Photo(),
                new Photo()
            };

            ICollection<DbEntityEntry> entries = new List<DbEntityEntry>();
            foreach (var entity in entities)
            {
                dbSetWrapper.Add(entity);

                entries.Add(dbSetWrapper.AttachIfDetached(entity));
            }

            // Act
            dbSetWrapper.AddMany(entities);

            // Assert
            foreach (var entry in entries)
            {
                Assert.That(entry.State, Is.EqualTo(EntityState.Added));
            }
        }
    }
}
