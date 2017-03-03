using Bytes2you.Validation;
using Visions.Data.Contracts;

namespace Visions.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IVisionsDbContext context;

        public UnitOfWork(IVisionsDbContext context)
        {
            Guard.WhenArgument(context, "context").IsNull().Throw();
            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }

        public void Dispose()
        {

        }
    }
}
