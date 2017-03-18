using System.Linq;
using Visions.Models.Models;

namespace Visions.Services.Contracts
{
    public interface IArticleService
    {
        Article Create(string title, string content, string userId);

        IQueryable<Article> GetAll();
    }
}
