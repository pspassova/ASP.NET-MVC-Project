using System.Data.Entity.Infrastructure;
using Visions.Data.Contracts;

namespace Visions.Data.Factories
{
    public interface IStatefulFactory
    {
        IStateful<T> CreateStateful<T>(DbEntityEntry<T> entry) where T : class;
    }
}
