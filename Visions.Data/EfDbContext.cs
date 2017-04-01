using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Visions.Data.Contracts;
using Visions.Models.Models;

namespace Visions.Data
{
    public class EfDbContext : IdentityDbContext<User>, IEfDbContext, IVisionsData
    {
        public EfDbContext()
            : base("Visions")
        {
        }

        public IDbSet<Article> Articles
        {
            get; set;
        }

        public IDbSet<Photo> Photos
        {
            get; set;
        }

        public IDbSet<Tag> Tags
        {
            get; set;
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public static EfDbContext Create()
        {
            return new EfDbContext();
        }
    }
}

