using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Visions.Models.Models;
using Visions.Services.Enumerations;

namespace Visions.Services.Contracts
{
    public interface IPhotoService
    {
        Photo Create(string userId, string path, ICollection<Tag> tags);

        Photo GetById(Guid id);

        IEnumerable<Photo> GetAll();

        IEnumerable<Photo> GetAll(string userId);

        IEnumerable<T1> GetAll<T, T1>(string userId, Expression<Func<Photo, T>> orderByProperty, OrderBy? order, Expression<Func<Photo, T1>> selectAs);

        IEnumerable<Photo> SortByTag(string tag, string userId = "");
    }
}
