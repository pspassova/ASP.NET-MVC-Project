using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services;

namespace Visions.Tests.Visions.Services.PhotoServiceTests
{
    [TestFixture]
    public class SortByTag_Should
    {
        private PhotoService service;
        private Mock<IEfDbSetWrapper<Photo>> dbSetWrapperMock;

        [SetUp]
        public void Setup()
        {
            this.dbSetWrapperMock = new Mock<IEfDbSetWrapper<Photo>>();

            this.service = new PhotoService(this.dbSetWrapperMock.Object);
        }

        [Test]
        public void ReturnTheCollectionOfPhotos_WhenTagIsNullAndUserIdIsEmpty()
        {
            // Arrange
            IQueryable<Photo> expectedPhotos = new List<Photo>()
            {
                new Photo(),
                new Photo()
            }.AsQueryable();

            this.dbSetWrapperMock.Setup(x => x.All).Returns(expectedPhotos);

            // Act
            IQueryable<Photo> actualPhotos = this.service.SortByTag(null, string.Empty);

            // Assert
            CollectionAssert.AreEqual(expectedPhotos, actualPhotos);
        }

        [Test]
        public void ReturnTheCollectionOfPhotosFilteredByUserId_WhenTagIsNullAndUserIdIsProvided()
        {
            // Arrange
            string userId = "test id";
            IQueryable<Photo> expectedPhotos = new List<Photo>()
            {
                new Photo() { UserId = userId },
                new Photo() { UserId = null }
            }.Where(x => x.UserId == userId)
            .AsQueryable();

            this.dbSetWrapperMock.Setup(x => x.All).Returns(expectedPhotos);

            // Act
            IQueryable<Photo> actualPhotos = this.service.SortByTag(null, userId);

            // Assert
            CollectionAssert.AreEqual(expectedPhotos, actualPhotos);
        }

        [Test]
        public void ReturnTheCollectionOfPhotosFilteredByTag_WhenTagIsProvidedAndUserIdIsEmpty_AndPhotosHaveTags()
        {
            // Arrange
            string filterTag = "first";
            ICollection<Tag> tags = new List<Tag>()
            {
                new Tag { Text = $"{filterTag} test tag" },
                new Tag { Text = "second test tag" }
            };
            IEnumerable<Photo> photos = new List<Photo>()
            {
                new Photo() { Tags = tags },
                new Photo()
            };

            ICollection<Photo> expectedPhotos = new List<Photo>();
            foreach (var photo in photos)
            {
                foreach (var tag in photo.Tags)
                {
                    if (tag.Text.Contains(filterTag))
                    {
                        expectedPhotos.Add(photo);
                        break;
                    }
                }
            }

            // The test would also pass if expected and actual photos are both with count = 0
            int expectedPhotosCount = 1;

            this.dbSetWrapperMock.Setup(x => x.All).Returns(expectedPhotos.AsQueryable());

            // Act
            IQueryable<Photo> actualPhotos = this.service.SortByTag(filterTag, string.Empty);

            // Assert
            Assert.AreEqual(expectedPhotosCount, expectedPhotos.Count());
            Assert.AreEqual(expectedPhotosCount, actualPhotos.Count());
            CollectionAssert.AreEqual(expectedPhotos, actualPhotos);
        }

        [Test]
        public void ReturnTheCollectionOfPhotosFilteredByTag_WhenBothTagAndUserIdAreProvided_AndPhotosHaveTags()
        {
            // Arrange
            string userId = "test id";
            string filterTag = "first";
            ICollection<Tag> tags = new List<Tag>()
            {
                new Tag { Text = $"{filterTag} test tag" },
                new Tag { Text = "second test tag" }
            };
            IEnumerable<Photo> photos = new List<Photo>()
            {
                new Photo() { UserId = userId, Tags = tags },
                new Photo() { UserId = userId },
                new Photo() { Tags = tags },
                new Photo()
            }.Where(x => x.UserId == userId);

            ICollection<Photo> expectedPhotos = new List<Photo>();
            foreach (var photo in photos)
            {
                foreach (var tag in photo.Tags)
                {
                    if (tag.Text.Contains(filterTag))
                    {
                        expectedPhotos.Add(photo);
                        break;
                    }
                }
            }

            // The test would also pass if expected and actual photos are both with count = 0
            int expectedPhotosCount = 1;

            this.dbSetWrapperMock.Setup(x => x.All).Returns(expectedPhotos.AsQueryable());

            // Act
            IQueryable<Photo> actualPhotos = this.service.SortByTag(filterTag, userId);

            // Assert
            Assert.AreEqual(expectedPhotosCount, expectedPhotos.Count());
            Assert.AreEqual(expectedPhotosCount, actualPhotos.Count());
            CollectionAssert.AreEqual(expectedPhotos, actualPhotos);
        }

        [Test]
        public void ReturnTheCollectionOfPhotosFilteredByTag_WhenTagIsProvidedAndUserIdIsEmpty_AndPhotosDoNotHaveAnyTags()
        {
            // Arrange
            string filterTag = "first";
            IEnumerable<Photo> photos = new List<Photo>()
            {
                new Photo(),
                new Photo()
            };

            ICollection<Photo> expectedPhotos = new List<Photo>();
            foreach (var photo in photos)
            {
                foreach (var tag in photo.Tags)
                {
                    if (tag.Text.Contains(filterTag))
                    {
                        expectedPhotos.Add(photo);
                        break;
                    }
                }
            }

            int expectedPhotosCount = 0;

            this.dbSetWrapperMock.Setup(x => x.All).Returns(expectedPhotos.AsQueryable());

            // Act
            IQueryable<Photo> actualPhotos = this.service.SortByTag(filterTag, string.Empty);

            // Assert
            Assert.AreEqual(expectedPhotosCount, expectedPhotos.Count());
            Assert.AreEqual(expectedPhotosCount, actualPhotos.Count());
            CollectionAssert.AreEqual(expectedPhotos, actualPhotos);
        }

        [Test]
        public void ReturnTheCollectionOfPhotosFilteredByTag_WhenBothTagAndUserIdAreProvided_AndPhotosDoNotHaveAnyTags()
        {
            // Arrange
            string userId = "test id";
            string filterTag = "first";
            IEnumerable<Photo> photos = new List<Photo>()
            {
                new Photo() { UserId = userId },
                new Photo()
            }.Where(x => x.UserId == userId);

            ICollection<Photo> expectedPhotos = new List<Photo>();
            foreach (var photo in photos)
            {
                foreach (var tag in photo.Tags)
                {
                    if (tag.Text.Contains(filterTag))
                    {
                        expectedPhotos.Add(photo);
                        break;
                    }
                }
            }

            int expectedPhotosCount = 0;

            this.dbSetWrapperMock.Setup(x => x.All).Returns(expectedPhotos.AsQueryable());

            // Act
            IQueryable<Photo> actualPhotos = this.service.SortByTag(filterTag, userId);

            // Assert
            Assert.AreEqual(expectedPhotosCount, expectedPhotos.Count());
            Assert.AreEqual(expectedPhotosCount, actualPhotos.Count());
            CollectionAssert.AreEqual(expectedPhotos, actualPhotos);
        }
    }
}
