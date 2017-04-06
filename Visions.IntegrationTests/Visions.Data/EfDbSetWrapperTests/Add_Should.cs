using Ninject;
using NUnit.Framework;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Web.App_Start;

namespace Visions.IntegrationTests.Visions.Data.EfDbSetWrapperTests
{
    [TestFixture]
    public class Add_Should
    {
        private static IKernel kernel;

        [SetUp]
        public void Setup()
        {
            kernel = NinjectWebCommon.CreateKernel();
        }

        [Test]
        public void ChangeEntrysEntityStateToAdded_WhenItIsProvided()
        {
            // Arrange
            IEfDbSetWrapper<Tag> dbSetWrapper = kernel.Get<IEfDbSetWrapper<Tag>>();

            Tag entity = new Tag();
            DbEntityEntry entry = dbSetWrapper.AttachIfDetached(entity);

            // Act
            dbSetWrapper.Add(entity);

            // Assert
            Assert.That(entry.State, Is.EqualTo(EntityState.Added));
        }
    }
}
