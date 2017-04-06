using Ninject;
using NUnit.Framework;
using System.Linq;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Web.App_Start;

namespace Visions.IntegrationTests.Visions.Services.ArticleServiceTests
{
    [TestFixture]
    public class GetAll_Should
    {
        private static IKernel kernel;

        private Article testArticle = new Article
        {
            Title = "test title",
            Content = "test content"
        };

        [SetUp]
        public void Setup()
        {
            kernel = NinjectWebCommon.CreateKernel();
        }

        [Test]
        public void ReturnAllTheArticlesFromDbSetWrapper()
        {
            // Arrange
            IArticleService service = kernel.Get<IArticleService>();
            IEfDbSetWrapper<Article> dbSetWrapper = kernel.Get<IEfDbSetWrapper<Article>>();

            IQueryable<Article> expectedArticles = dbSetWrapper.All;

            // Act
            IQueryable<Article> actualArticles = service.GetAll();

            // Assert
            Assert.That(actualArticles.Count(), Is.EqualTo(expectedArticles.Count()));
        }

        [Test]
        public void ReturnAllTheArticlesFromDatabase()
        {
            // Arrange
            IArticleService service = kernel.Get<IArticleService>();

            // Act
            IQueryable<Article> returnedArticles = service.GetAll();

            // Assert
            Assert.IsNotEmpty(returnedArticles);
        }
    }
}
