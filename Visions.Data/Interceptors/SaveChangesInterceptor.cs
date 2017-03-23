using Ninject.Extensions.Interception;
using Visions.Data.Contracts;

namespace Visions.Data.Interceptors
{
    public class SaveChangesInterceptor : IInterceptor
    {
        private readonly IVisionsDbContext context;

        public SaveChangesInterceptor(IVisionsDbContext context)
        {
            this.context = context;
        }

        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
            this.context.SaveChanges();
        }
    }
}
