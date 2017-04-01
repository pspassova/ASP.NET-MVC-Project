using System.Data.Entity;
using Visions.Models.Models;

namespace Visions.Data.Contracts
{
    public interface IVisionsData
    {
        IDbSet<Article> Articles
        {
            get;
        }

        IDbSet<Photo> Photos
        {
            get;
        }

        IDbSet<Tag> Tags
        {
            get;
        }
    }
}
