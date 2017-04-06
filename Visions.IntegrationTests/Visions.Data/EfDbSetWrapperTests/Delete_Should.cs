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
    public class Delete_Should
    {
        private static IKernel kernel;

        [SetUp]
        public void Setup()
        {
            kernel = NinjectWebCommon.CreateKernel();
        }

        [Test]
        public void ChangeEntrysEntityStateToDeleted_WhenItIsProvided()
        {
            // Arrange
            IEfDbSetWrapper<Article> dbSetWrapper = kernel.Get<IEfDbSetWrapper<Article>>();

            Article entity = new Article();
            DbEntityEntry entry = dbSetWrapper.AttachIfDetached(entity);

            // Act
            dbSetWrapper.Delete(entity);

            // Assert
            Assert.That(entry.State, Is.EqualTo(EntityState.Deleted));
        }
    }
}
