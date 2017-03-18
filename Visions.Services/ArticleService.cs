using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;

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
            return this.repository.GetAll();
        }
    }
}
