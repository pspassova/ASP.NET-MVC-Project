using System.Data.Entity;

namespace Visions.Data.Contracts
{
    public interface IStateful<T>
        where T : class
    {
        EntityState EntityState { get; set; }
    }
}
