using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Visions.Data.Contracts
{
    public interface IEfDbSetWrapper<T>
        where T : class
    {
        T GetById(object id);

        IQueryable<T> All { get; }

        void Add(T entity);

        void AddMany(IEnumerable<T> entities);

        void Update(T entity);

        void Delete(T entity);

        DbEntityEntry AttachIfDetached(T entity);
    }
}
