using Bytes2you.Validation;
using System.Collections.Generic;
using System.Data.Entity;
using Visions.Data.Contracts;

namespace Visions.Data
{
    public class EfRepository<T> : IEfRepository<T>
        where T : class
    {
        public EfRepository(IVisionsDbContext context)
        {
            Guard.WhenArgument(context, "context").IsNull().Throw();

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        public IVisionsDbContext Context { get; set; }

        public IDbSet<T> DbSet { get; set; }

        public void Add(T entity)
        {
            Guard.WhenArgument(entity, "entity").IsNull().Throw();

            IStateful<T> entry = this.Context.GetStateful(entity);
            entry.EntityState = EntityState.Added;
        }

        public void Update(T entity)
        {
            Guard.WhenArgument(entity, "entity").IsNull().Throw();

            IStateful<T> entry = this.Context.GetStateful(entity);
            entry.EntityState = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            Guard.WhenArgument(entity, "entity").IsNull().Throw();

            IStateful<T> entry = this.Context.GetStateful(entity);
            entry.EntityState = EntityState.Deleted;
        }

        public T GetById(object id)
        {
            Guard.WhenArgument(id, "id").IsNull().Throw();

            return this.DbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return this.DbSet;
        }

        public IStateful<T> AttachIfDetached(T entity)
        {
            Guard.WhenArgument(entity, "entity").IsNull().Throw();

            IStateful<T> entry = this.Context.GetStateful(entity);
            if (entry.EntityState == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            return entry;
        }
    }
}
