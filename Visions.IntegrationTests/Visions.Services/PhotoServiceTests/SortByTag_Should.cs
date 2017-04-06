using Ninject;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Web.App_Start;

namespace Visions.IntegrationTests.Visions.Services.PhotoServiceTests
{
    [TestFixture]
    public class SortByTag_Should
    {
        private static IKernel kernel;

        [SetUp]
        public void Setup()
        {
            kernel = NinjectWebCommon.CreateKernel();
        }

        [TestCase("tag")]
        [TestCase("pur")]
        [TestCase("weird")]
        public void ReturnTheCorrectPhotosFromDbSetWrapperFilteredByTag_WhenItIsProvided(string filterTag)
        {
            // Arrange
            IPhotoService service = kernel.Get<IPhotoService>();
            IEfDbSetWrapper<Photo> dbSetWrapper = kernel.Get<IEfDbSetWrapper<Photo>>();

            IEnumerable<Photo> dbSetWrapperPhotos = dbSetWrapper.All;

            ICollection<Photo> expectedPhotos = new List<Photo>();
            foreach (var photo in dbSetWrapperPhotos)
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

            // Act
            IQueryable<Photo> actualPhotos = service.SortByTag(filterTag, string.Empty);

            // Assert
            Assert.That(actualPhotos.Count(), Is.EqualTo(expectedPhotos.Count()));
        }
    }
}
