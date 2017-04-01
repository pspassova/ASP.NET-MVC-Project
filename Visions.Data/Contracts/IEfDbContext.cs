using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Visions.Data.Contracts
{
    public interface IEfDbContext
    {
        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        int SaveChanges();
    }
}
