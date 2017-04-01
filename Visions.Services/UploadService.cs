using System.Collections.Generic;
using Bytes2you.Validation;
using Visions.Data.Contracts;
using Visions.Services.Contracts;

namespace Visions.Services
{
    public class UploadService<T> : IUploadService<T>
        where T : class
    {
        private readonly IEfDbSetWrapper<T> dbSetWrapper;
        private readonly IEfDbContextSaveChanges dbContextSaveChanges;

        public UploadService(IEfDbSetWrapper<T> dbSetWrapper, IEfDbContextSaveChanges dbContextSaveChanges)
        {
            Guard.WhenArgument(dbSetWrapper, "dbSetWrapper").IsNull().Throw();
            Guard.WhenArgument(dbContextSaveChanges, "dbContextSaveChanges").IsNull().Throw();

            this.dbSetWrapper = dbSetWrapper;
            this.dbContextSaveChanges = dbContextSaveChanges;
        }

        public void UploadToDatabase(T item)
        {
            Guard.WhenArgument(item, "item").IsNull().Throw();

            this.dbSetWrapper.Add(item);
            this.dbContextSaveChanges.SaveChanges();
        }

        public void UploadManyToDatabase(IEnumerable<T> items)
        {
            Guard.WhenArgument(items, "items").IsNull().Throw();

            this.dbSetWrapper.AddMany(items);
            this.dbContextSaveChanges.SaveChanges();
        }
    }
}
