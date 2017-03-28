using Bytes2you.Validation;
using Ninject.Extensions.Interception;
using Visions.Data.Contracts;

namespace Visions.Data.Interceptors
{
    public class SaveChangesInterceptor : IInterceptor
    {
        private readonly IEfDbContext context;

        public SaveChangesInterceptor(IEfDbContext context)
        {
            Guard.WhenArgument(context, "context").IsNull().Throw();

            this.context = context;
        }

        public void Intercept(IInvocation invocation)
        {
            Guard.WhenArgument(invocation, "invocation").IsNull().Throw();

            invocation.Proceed();
            this.context.SaveChanges();
        }
    }
}
