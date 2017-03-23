using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        void InitializeDb();

        void SaveChanges();
    }
}
