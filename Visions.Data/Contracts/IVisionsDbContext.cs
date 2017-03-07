using System.Data.Entity;
using Visions.Models.Models;

namespace Visions.Data.Contracts
{
    public interface IVisionsDbContext
    {
        IDbSet<Photo> Photos { get; }

        IDbSet<Article> Articles { get; }

        IDbSet<T> Set<T>() where T : class;

        IStateful<T> GetStateful<T>(T entity) where T : class;

        void InitializeDb();

        void SaveChanges();
    }
}
