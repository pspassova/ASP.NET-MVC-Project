using Bytes2you.Validation;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Visions.Data.Contracts;

namespace Visions.Data
{
    public class Stateful<T> : IStateful<T>
        where T : class
    {
        private readonly DbEntityEntry<T> entry;

        public Stateful(DbEntityEntry<T> entry)
        {
            Guard.WhenArgument(entry, "entry").IsNull().Throw();

            this.entry = entry;
        }

        public EntityState EntityState { get; set; }
    }
}
