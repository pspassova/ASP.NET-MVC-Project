using PagedList;
using System.Collections.Generic;

namespace Visions.Web.Common.Contracts
{
    public interface IPagingProvider<T>
        where T : class
    {
        IPagedList<T> CreatePagedList(IEnumerable<T> items, int pageNumber, int pageSize);
    }
}
