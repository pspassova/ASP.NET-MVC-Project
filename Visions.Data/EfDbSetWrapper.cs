using Bytes2you.Validation;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Visions.Data.Contracts;
using System.Collections.Generic;

namespace Visions.Data
{
    public class EfDbSetWrapper<T> : IEfDbSetWrapper<T>
        where T : class
    {
        public EfDbSetWrapper(IEfDbContext context)
        {
            Guard.WhenArgument(context, "context").IsNull().Throw();

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        public IEfDbContext Context
        {
            get; set;
        }

        public IDbSet<T> DbSet
        {
            get; set;
        }

        public void Add(T entity)
        {
            Guard.WhenArgument(entity, "entity").IsNull().Throw();

            DbEntityEntry entry = this.AttachIfDetached(entity);
            entry.State = EntityState.Added;
        }

        public void AddMany(IEnumerable<T> entities)
        {
            Guard.WhenArgument(entities, "entities").IsNull().Throw();

            foreach (var entity in entities)
            {
                this.Add(entity);
            }
        }

        public void Update(T entity)
        {
            Guard.WhenArgument(entity, "entity").IsNull().Throw();

            DbEntityEntry entry = AttachIfDetached(entity);
            entry.State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            Guard.WhenArgument(entity, "entity").IsNull().Throw();

            DbEntityEntry entry = AttachIfDetached(entity);
            entry.State = EntityState.Deleted;
        }

        public T GetById(object id)
        {
            Guard.WhenArgument(id, "id").IsNull().Throw();

            return this.DbSet.Find(id);
        }

        public IQueryable<T> All
        {
            get
            {
                return this.DbSet;
            }
        }

        public DbEntityEntry AttachIfDetached(T entity)
        {
            Guard.WhenArgument(entity, "entity").IsNull().Throw();

            DbEntityEntry entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            return entry;
        }
    }
}
