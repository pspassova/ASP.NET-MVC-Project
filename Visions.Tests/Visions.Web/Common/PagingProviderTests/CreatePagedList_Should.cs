using Moq;
using NUnit.Framework;
using PagedList;
using System.Collections.Generic;
using Visions.Models.Models;
using Visions.Web.Common;
using Visions.Web.Common.Contracts;

namespace Visions.Tests.Visions.Web.Common.PagingProviderTests
{
    [TestFixture]
    public class CreatePagedList_Should
    {
        private IPagingProvider<Tag> pagingProvider;

        [SetUp]
        public void Setup()
        {
            this.pagingProvider = new PagingProvider<Tag>();
        }

        [Test]
        public void ReturnAnInstanceOfIPagedList()
        {
            // Act, Assert
            Assert.IsInstanceOf<IPagedList>(this.pagingProvider.CreatePagedList(
                It.IsAny<ICollection<Tag>>(),
                It.IsAny<int>(),
                It.IsAny<int>()));
        }

        [Test]
        public void SetPageNumberToOne_WhenPassedPassedPageNumberIsNotValid()
        {
            // Arrange
            int validPageNumber = 1;
            int invalidPageNumber = 0;

            // Act
            IPagedList<Tag> result = this.pagingProvider.CreatePagedList(
                It.IsAny<ICollection<Tag>>(),
                invalidPageNumber,
                It.IsAny<int>());

            // Assert
            Assert.AreEqual(validPageNumber, result.PageNumber);
        }

        [Test]
        public void SetPageSizeToOne_WhenPassedPassedPageSizeIsNotValid()
        {
            // Arrange
            int validPageSize = 1;
            int invalidPageSize = 0;

            // Act
            IPagedList<Tag> result = this.pagingProvider.CreatePagedList(
                It.IsAny<ICollection<Tag>>(),
                It.IsAny<int>(),
                invalidPageSize);

            // Assert
            Assert.AreEqual(validPageSize, result.PageSize);
        }

        [Test]
        public void ReturnTheCorrectCountOfItems_WhenItemsAreProvided()
        {
            // Assert
            var testData = new List<Tag>
            {
                new Mock<Tag>().Object,
                new Mock<Tag>().Object
            };

            // Act
            IPagedList result = this.pagingProvider.CreatePagedList(
                testData,
                It.IsAny<int>(),
                It.IsAny<int>());

            // Assert
            Assert.AreEqual(testData.Count, result.TotalItemCount);
        }

        [TestCase(1, 4)]
        [TestCase(23, 5)]
        public void SetTheCorrectValuesToAllPropertiesOfTheReturnedPagedList_WhenAllParametersAreProvided(int pageNumber, int pageSize)
        {
            // Assert
            var testDataItems = new List<Tag>
            {
                new Mock<Tag>().Object,
                new Mock<Tag>().Object,
                new Mock<Tag>().Object,
                new Mock<Tag>().Object
            };

            // Act
            IPagedList result = this.pagingProvider.CreatePagedList(
                testDataItems,
                pageNumber,
                pageSize);

            // Assert
            Assert.AreEqual(testDataItems.Count, result.TotalItemCount);
            Assert.AreEqual(pageNumber, result.PageNumber);
            Assert.AreEqual(pageSize, result.PageSize);
        }
    }
}
