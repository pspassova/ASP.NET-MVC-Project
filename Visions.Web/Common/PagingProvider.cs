using System.Collections.Generic;
using PagedList;
using Visions.Web.Common.Contracts;

namespace Visions.Web.Common
{
    public class PagingProvider<T> : IPagingProvider<T>
        where T : class
    {
        public IPagedList<T> CreatePagedList(IEnumerable<T> items, int pageNumber, int pageSize)
        {
            if (items == null)
            {
                items = new List<T>();
            }

            if (pageNumber == 0)
            {
                pageNumber = 1;
            }

            if (pageSize == 0)
            {
                pageSize = 1;
            }

            return new PagedList<T>(items, pageNumber, pageSize);
        }
    }
}