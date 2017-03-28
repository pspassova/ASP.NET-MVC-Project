using System.Collections.Generic;
using Bytes2you.Validation;
using Visions.Data.Contracts;
using Visions.Services.Contracts;

namespace Visions.Services
{
    public class UploadService<T> : IUploadService<T>
        where T : class
    {
        private readonly IEfDbSetWrapper<T> repository;

        public UploadService(IEfDbSetWrapper<T> repository)
        {
            Guard.WhenArgument(repository, "repository").IsNull().Throw();

            this.repository = repository;
        }

        public void UploadToDatabase(T item)
        {
            Guard.WhenArgument(item, "item").IsNull().Throw();

            this.repository.Add(item);
        }

        public void UploadManyToDatabase(IEnumerable<T> items)
        {
            Guard.WhenArgument(items, "items").IsNull().Throw();

            this.repository.AddMany(items);
        }
    }
}
