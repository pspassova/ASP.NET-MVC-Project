using System.Collections.Generic;

namespace Visions.Services.Contracts
{
    public interface IUploadService<T> 
        where T : class
    {
        void Upload(T item);

        void Upload(IEnumerable<T> items);
    }
}
