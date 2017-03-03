using Bytes2you.Validation;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Visions.Data.Contracts;

namespace Visions.Data
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        public GenericRepository(IVisionsDbContext context)
        {
            Guard.WhenArgument(context, "context").IsNull();

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        public IVisionsDbContext Context { get; set; }

        public IDbSet<T> DbSet { get; set; }

        public void Add(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            entry.State = EntityState.Added;
        }

        public void Update(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            entry.State = EntityState.Deleted;
        }

        public T GetById(object id)
        {
            return this.DbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return this.DbSet;
        }

        private DbEntityEntry AttachIfDetached(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            return entry;
        }
    }
}
