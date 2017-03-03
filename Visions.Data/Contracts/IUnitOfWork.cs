using System;

namespace Visions.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
