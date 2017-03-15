using System.Collections.Generic;

namespace Visions.Data.Contracts
{
    public interface IEfRepository<T>
        where T : class
    {
        T GetById(object id);

        IEnumerable<T> GetAll();

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        IStateful<T> AttachIfDetached(T entity);
    }
}
