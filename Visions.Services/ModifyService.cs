using Bytes2you.Validation;
using Visions.Data.Contracts;
using Visions.Services.Contracts;

namespace Visions.Services
{
    public class ModifyService<T> : IModifyService<T>
        where T : class
    {
        private readonly IEfDbSetWrapper<T> dbSetWrapper;
        private readonly IEfDbContextSaveChanges dbContextSaveChanges;

        public ModifyService(IEfDbSetWrapper<T> dbSetWrapper, IEfDbContextSaveChanges dbContextSaveChanges)
        {
            Guard.WhenArgument(dbSetWrapper, "dbSetWrapper").IsNull().Throw();
            Guard.WhenArgument(dbContextSaveChanges, "dbContextSaveChanges").IsNull().Throw();

            this.dbSetWrapper = dbSetWrapper;
            this.dbContextSaveChanges = dbContextSaveChanges;
        }

        public void Edit(T item)
        {
            Guard.WhenArgument(item, "item").IsNull().Throw();

            this.dbSetWrapper.Update(item);
            this.dbContextSaveChanges.SaveChanges();
        }
    }
}
