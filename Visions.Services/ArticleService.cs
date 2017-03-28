using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IQueryable<Article> GetAll()
        {
            return this.repository.All;
        }

        public IQueryable<Article> GetAllOrderedByCreatedOn(OrderBy? order, string userId = "")
        {
            if (userId == string.Empty)
            {
                if (order == null || order == OrderBy.Ascending)
                {
                    return this.GetAll();
                }
                else
                {
                    return this.GetAll().OrderByDescending(x => x.CreatedOn);
                }
            }
            else
            {
                if (order == null || order == OrderBy.Ascending)
                {
                    return this.GetAll().Where(x => x.UserId == userId);
                }
                else
                {
                    return this.GetAll().Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedOn);
                }
            }
        }
    }
}
