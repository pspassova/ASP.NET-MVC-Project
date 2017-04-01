using Bytes2you.Validation;
using Visions.Data.Contracts;

namespace Visions.Data
{
    public class EfDbContextSaveChanges : IEfDbContextSaveChanges
    {
        private readonly IEfDbContext dbContext;

        public EfDbContextSaveChanges(IEfDbContext dbContext)
        {
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();

            this.dbContext = dbContext;
        }
        public int SaveChanges()
        {
            return this.dbContext.SaveChanges();
        }
    }
}
