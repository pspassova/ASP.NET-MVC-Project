using System.Data.Entity;
using Visions.Models.Models;

namespace Visions.Data.Contracts
{
    public interface IVisionsDbContext
    {
        IDbSet<Article> Articles
        {
            get;
        }

        IDbSet<Photo> Photos
        {
            get;
        }

        IDbSet<Tag> Tags
        {
            get;
        }

        IDbSet<T> Set<T>() where T : class;

        IStateful<T> GetStateful<T>(T entity) where T : class;

        void InitializeDb();

        void SaveChanges();
    }
}
