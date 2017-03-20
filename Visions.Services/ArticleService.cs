using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Services.Enumerations;

namespace Visions.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IEfRepository<Article> repository;

        public ArticleService(IEfRepository<Article> repository)
        {
            Guard.WhenArgument(repository, "repository").IsNull().Throw();

            this.repository = repository;
        }

        public Article Create(string title, string content, string userId)
        {
            return new Article()
            {
                Id = Guid.NewGuid(),
                Title = title,
                Content = content,
                UserId = userId,
                CreatedOn = DateTime.UtcNow,
                Tags = new List<Tag>()
            };
        }

        public IEnumerable<Article> GetAll()
        {
            return this.repository.GetAll();
        }

        public IEnumerable<T1> GetAll<T, T1>(Expression<Func<Article, T>> orderByProperty, OrderBy? order, Expression<Func<Article, T1>> selectAs)
        {
            IQueryable<Article> result = this.repository.All;

            if (orderByProperty != null)
            {
                if (order == OrderBy.Ascending || order == null)
                {
                    result = result.OrderBy(orderByProperty);
                }
                else if (order == OrderBy.Descending)
                {
                    result = result.OrderByDescending(orderByProperty);
                }
            }

            if (selectAs != null)
            {
                return result.Select(selectAs).ToList();
            }
            else
            {
                return result.OfType<T1>().ToList();
            }
        }
    }
}
