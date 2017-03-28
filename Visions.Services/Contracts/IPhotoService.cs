using System;
using System.Collections.Generic;
using System.Linq;
using Visions.Models.Models;
using Visions.Services.Enumerations;

namespace Visions.Services.Contracts
{
    public interface IPhotoService
    {
        Photo Create(string userId, string path, ICollection<Tag> tags);

        Photo GetById(Guid id);

        IQueryable<Photo> GetAll();

        IQueryable<Photo> GetAllByUserId(string userId);

        IQueryable<Photo> GetAllOrderedByCreatedOn(OrderBy? order, string userId = "");

        IEnumerable<Photo> SortByTag(string tag, string userId = "");
    }
}
