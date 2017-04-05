using Ninject;
using NUnit.Framework;
using System.Linq;
using Visions.Data;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services;
using Visions.Services.Contracts;
using Visions.Web.App_Start;

namespace Visions.IntegrationTests.Visions.Services.ArticleServiceTests
{
    [TestFixture]
    public class GetAll_Should
    {
        private static IKernel kernel;

        private EfDbContext dbContext;
        private IEfDbContextSaveChanges dbContextSaveChanges;
        private IEfDbSetWrapper<Article> articleDbSetWrapper;

        private Article testArticle = new Article
        {
            Title = "test title",
            Content = "test content"
        };

        [SetUp]
        public void Setup()
        {
            kernel = NinjectWebCommon.CreateKernel();

            this.dbContext = kernel.Get<EfDbContext>();
            this.dbContextSaveChanges = kernel.Get<IEfDbContextSaveChanges>();
            this.articleDbSetWrapper = kernel.Get<IEfDbSetWrapper<Article>>();

            this.dbContext.Articles.Add(testArticle);
            this.dbContextSaveChanges.SaveChanges();
        }

        [TearDown]
        public void Teardown()
        {
            this.dbContext.Articles.Remove(testArticle);
            this.dbContextSaveChanges.SaveChanges();
        }

        [Test]
        public void ReturnAllTheArticlesFromDbSetWrapper()
        {
            // Arrange
            IArticleService service = new ArticleService(this.articleDbSetWrapper);

            // Act
            IQueryable<Article> actualArticles = service.GetAll();

            // Assert
            Assert.That(actualArticles, Is.SameAs(this.articleDbSetWrapper.All));
        }
    }
}
