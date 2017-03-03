using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Visions.Data.Contracts
{
    public interface IVisionsDbContext
    {
        void InitializeDb();

        void InitializeIdentity();

        void SaveChanges();

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
