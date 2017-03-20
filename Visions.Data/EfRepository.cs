using Bytes2you.Validation;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Visions.Data.Contracts;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

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

        public IVisionsDbContext Context
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

        public IEnumerable<T> GetAll()
        {
            return this.GetAll(null);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filterExpression)
        {
            return this.GetAll<object>(filterExpression, null);
        }

        public IEnumerable<T> GetAll<T1>(Expression<Func<T, bool>> filterExpression, Expression<Func<T, T1>> sortExpression)
        {
            return this.GetAll<T1, T>(filterExpression, sortExpression, null);
        }

        public IEnumerable<T2> GetAll<T1, T2>(Expression<Func<T, bool>> filterExpression, Expression<Func<T, T1>> sortExpression, Expression<Func<T, T2>> selectExpression)
        {
            IQueryable<T> result = this.DbSet;

            if (filterExpression != null)
            {
                result = result.Where(filterExpression);
            }

            if (sortExpression != null)
            {
                result = result.OrderBy(sortExpression);
            }

            if (selectExpression != null)
            {
                return result.Select(selectExpression).ToList();
            }
            else
            {
                return result.OfType<T2>().ToList();
            }
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
