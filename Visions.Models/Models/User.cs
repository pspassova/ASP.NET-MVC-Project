using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Visions.Models.Models
{
    public class User : IdentityUser
    {
        private ICollection<Photo> pinnedPhotos;
        private ICollection<Article> pinnedArticles;

        public User()
        {
            this.PinnedPhotos = new HashSet<Photo>();
            this.pinnedArticles = new HashSet<Article>();
        }

        public string AvatarPath { get; set; }

        public virtual ICollection<Photo> PinnedPhotos
        {
            get { return this.pinnedPhotos; }
            set { this.pinnedPhotos = value; }
        }

        public virtual ICollection<Article> PinnedArticles
        {
            get { return this.pinnedArticles; }
            set { this.pinnedArticles = value; }
        }

        public ClaimsIdentity GenerateUserIdentity(UserManager<User> manager)
        {
            var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            return Task.FromResult(GenerateUserIdentity(manager));
        }
    }
}
