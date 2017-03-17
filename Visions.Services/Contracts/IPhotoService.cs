using System.Collections.Generic;
using Visions.Models.Models;

namespace Visions.Services.Contracts
{
    public interface IPhotoService
    {
        IEnumerable<Photo> GetAll();

        IEnumerable<Photo> SortByTag(string tag);
    }
}
