using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Visions.Models.Models;

namespace Visions.Web.Models
{
    public class ArticleViewModel
    {
        public static Expression<Func<Article, ArticleViewModel>> FromArticle
        {
            get
            {
                return article => new ArticleViewModel
                {
                    Id = article.Id,
                    Title = article.Title,
                    Content = article.Content,
                    Tags = article.Tags,
                    UserId = article.UserId,
                    IsDeleted = article.IsDeleted,
                    CreatedOn = article.CreatedOn,
                };
            }
        }

        //public static Expression<Func<ArticleViewModel, Article>> ToArticle
        //{
        //    get
        //    {
        //        return articleViewModel => new Article
        //        {
        //            Id = articleViewModel.Id,
        //            Title = articleViewModel.Title,
        //            Content = articleViewModel.Content,
        //            Tags = articleViewModel.Tags,
        //            UserId = articleViewModel.UserId,
        //            IsDeleted = articleViewModel.IsDeleted,
        //            CreatedOn = articleViewModel.CreatedOn,
        //        };
        //    }
        //}

        public Guid Id
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }

        public string Content
        {
            get; set;
        }

        public string UserId
        {
            get; set;
        }

        public bool IsDeleted
        {
            get; set;
        }

        public DateTime? CreatedOn
        {
            get; set;
        }

        public ICollection<Tag> Tags
        {
            get; set;
        }
    }
}