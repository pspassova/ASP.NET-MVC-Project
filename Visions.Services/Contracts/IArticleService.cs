using System.Linq;
using Visions.Models.Models;
using Visions.Services.Enumerations;

namespace Visions.Services.Contracts
{
    public interface IArticleService
    {
        Article Create(string title, string content, string userId);

        IQueryable<Article> GetAll();

        IQueryable<Article> GetAllOrderedByCreatedOn(OrderBy? order, string userId = "");
    }
}
