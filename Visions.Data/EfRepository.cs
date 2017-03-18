﻿using Bytes2you.Validation;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
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

            DbEntityEntry entry = AttachIfDetached(entity);
            entry.State = EntityState.Added;
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

        public IQueryable<T> GetAll()
        {
            return this.DbSet;
        }

        private DbEntityEntry AttachIfDetached(T entity)
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