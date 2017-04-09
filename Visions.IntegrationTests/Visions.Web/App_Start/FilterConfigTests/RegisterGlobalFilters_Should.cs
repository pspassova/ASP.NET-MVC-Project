using NUnit.Framework;
using System.Web.Mvc;
using Visions.Web;

namespace Visions.Tests.Visions.Web.App_Start.FilterConfigTests
{
    [TestFixture]
    public class RegisterGlobalFilters_Should
    {
        [Test]
        public void AddTheRightCountOfFiltersToGlobalFilterCollection()
        {
            // Arrange

            // If we have any filters added throgh RegisterGlobalFilters in FilterConfig,
            // initial filterCollection's count will be positive
            GlobalFilterCollection filterCollection = new GlobalFilterCollection();
            int initialFilterCollectionCount = filterCollection.Count;

            FilterAttribute testFilter = new HandleErrorAttribute();
            filterCollection.Add(testFilter);
            filterCollection.Add(testFilter);

            int newAddedFiltersCount = 2;
            int resultFilterCollectionsCount = filterCollection.Count;

            // Act
            FilterConfig.RegisterGlobalFilters(filterCollection);

            // Assert
            Assert.That(resultFilterCollectionsCount == initialFilterCollectionCount + newAddedFiltersCount);
        }
    }
}
