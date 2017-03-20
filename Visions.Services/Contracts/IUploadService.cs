using System.Collections.Generic;

namespace Visions.Services.Contracts
{
    public interface IUploadService<T> 
        where T : class
    {
        void UploadToDatabase(T item);

        void UploadManyToDatabase(IEnumerable<T> items);
    }
}
