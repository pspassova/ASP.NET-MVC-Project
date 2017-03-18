using System.Collections.Generic;
using System.Linq;
using Visions.Models.Models;

namespace Visions.Services.Contracts
{
    public interface IPhotoService
    {
        Photo Create(string userId, string path);

        IQueryable<Photo> GetAll();

        IQueryable<Photo> GetAllForUser(string userId);

        IEnumerable<Photo> SortByTag(string tag);
    }
}
