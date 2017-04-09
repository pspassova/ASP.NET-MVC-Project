using NUnit.Framework;
using System.Web.Optimization;
using Visions.Web;

namespace Visions.IntegrationTests.Visions.Web.App_Start.BundleConfigTests
{
    [TestFixture]
    public class RegisterBundles_Should
    {
        [Test]
        public void AddTheRightCountOfBundlesToBundleCollection()
        {
            // Arrange

            // If we have any bundles added throgh RegisterBundles in BundleConfig,
            // initial bundlesCollection's count will be positive.
            BundleCollection bundlesCollection = new BundleCollection();
            int initialBundlesCollection = bundlesCollection.Count;

            Bundle testBundle = new Bundle("~/test path");
            bundlesCollection.Add(testBundle);

            int newAddedBundlesCollectionCount = 1;
            int resultBundlesCollectionCount = bundlesCollection.Count;

            // Act
            BundleConfig.RegisterBundles(bundlesCollection);

            // Assert
            Assert.That(resultBundlesCollectionCount == initialBundlesCollection + newAddedBundlesCollectionCount);
        }
    }
}
