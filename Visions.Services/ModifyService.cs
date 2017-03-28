using Bytes2you.Validation;
using Visions.Data.Contracts;
using Visions.Services.Contracts;

namespace Visions.Services
{
    public class ModifyService<T> : IModifyService<T>
        where T : class
    {
        private readonly IEfDbSetWrapper<T> repository;

        public ModifyService(IEfDbSetWrapper<T> repository)
        {
            Guard.WhenArgument(repository, "repository").IsNull().Throw();

            this.repository = repository;
        }

        public void Edit(T item)
        {
            Guard.WhenArgument(item, "item").IsNull().Throw();

            this.repository.Update(item);
        }
    }
}
