using Moq;
using NUnit.Framework;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services;
using Visions.Services.Contracts;

namespace Visions.Tests.Visions.Services.ArticleServiceTests
{
    [TestFixture]
    public class GetAll_Should
    {
        [Test]
        public void InvokeGetAllMethod_Once()
        {
            // Arrange
            var repositoryMock = new Mock<IEfRepository<Article>>();
            IArticleService articleService = new ArticleService(repositoryMock.Object);

            // Act
            articleService.GetAll();

            // Assert
            repositoryMock.Verify(x => x.GetAll(), Times.Once);
        }
    }
}
