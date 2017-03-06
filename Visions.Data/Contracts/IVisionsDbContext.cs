using System.Data.Entity;

namespace Visions.Data.Contracts
{
    public interface IVisionsDbContext
    {
        void InitializeDb();

        void SaveChanges();

        IStateful<T> GetStateful<T>(T entity) where T : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
