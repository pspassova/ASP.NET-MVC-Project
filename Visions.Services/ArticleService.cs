﻿using Bytes2you.Validation;
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
        private readonly IEfDbSetWrapper<Article> dbSetWrapper;

        public ArticleService(IEfDbSetWrapper<Article> dbSetWrapper)
        {
            Guard.WhenArgument(dbSetWrapper, "dbSetWrapper").IsNull().Throw();

            this.dbSetWrapper = dbSetWrapper;
        }

        public Article Create(string title, string content, string userId)
        {
            return new Article()
            {
                Title = title,
                Content = content,
                UserId = userId,
                CreatedOn = DateTime.UtcNow,
                Tags = new List<Tag>()
            };
        }

        public IQueryable<Article> GetAll()
        {
            return this.dbSetWrapper.All;
        }

        public IQueryable<Article> GetAllOrderedByCreatedOn(OrderBy? order, string userId = "")
        {
            if (userId == string.Empty)
            {
                if (order == null || order == OrderBy.Ascending)
                {
                    return this.GetAll().OrderBy(x => x.CreatedOn);
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
                    return this.GetAll().Where(x => x.UserId == userId).OrderBy(x => x.CreatedOn);
                }
                else
                {
                    return this.GetAll().Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedOn);
                }
            }
        }
    }
}
