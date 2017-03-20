using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Visions.Models.Models;
using Visions.Services.Enumerations;

namespace Visions.Services.Contracts
{
    public interface IArticleService
    {
        Article Create(string title, string content, string userId);

        IEnumerable<Article> GetAll();

        IEnumerable<T1> GetAll<T, T1>(Expression<Func<Article, T>> orderByProperty, OrderBy? order, Expression<Func<Article, T1>> selectAs);
    }
}
