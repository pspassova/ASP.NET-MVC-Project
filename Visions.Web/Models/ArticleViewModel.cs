using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

        public Guid Id
        {
            get; set;
        }

        //[StringLength(50, MinimumLength = 2, ErrorMessage = "Enter at least {0} characters.")]
        public string Title
        {
            get; set;
        }

        //[StringLength(500, MinimumLength = 2, ErrorMessage = "Enter at least {0} characters.")]
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