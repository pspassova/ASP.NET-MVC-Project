using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Visions.Data.Contracts;
using Visions.Models.Models;

namespace Visions.Data
{
    public class VisionsDbContext : IdentityDbContext<User>, IVisionsDbContext
    {
        public VisionsDbContext()
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

        public static VisionsDbContext Create()
        {
            return new VisionsDbContext();
        }

        public void InitializeDb()
        {
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        //private void InitializeIdentity()
        //{
        //    var roleStore = new RoleStore<IdentityRole>(this);
        //    var roleManager = new RoleManager<IdentityRole>(roleStore);
        //    var userStore = new UserStore<User>(this);
        //    var userManager = new UserManager<User>(userStore);

        //    // Add missing roles
        //    var role = roleManager.FindByName("Admin");
        //    if (role == null)
        //    {
        //        role = new IdentityRole("Admin");
        //        roleManager.Create(role);
        //    }

        //    // Create admin
        //    User user = userManager.FindByEmail("admin@admin.com");
        //    userManager.AddToRole(user.Id, "Admin");
        //}

        //private void SeedTags()
        //{
        //    this.Tags.AddOrUpdate(t => t.Text,
        //        new Tag()
        //        {
        //            Id = Guid.NewGuid(),
        //            Text = "sea",
        //            Photos = new List<Photo>()
        //            {
        //                new Photo
        //        {
        //            Id = Guid.NewGuid(),
        //            Path = "\\Images\\sea.jpg",
        //            UserId = "47964424-53cb-431c-bdad-023379b4a7df",
        //            CreatedOn = DateTime.Now,
        //            IsDeleted = false,
        //            Likes = 1
        //        },
        //        new Photo
        //        {
        //            Id = Guid.NewGuid(),
        //            Path = "/Images/rain.jpg",
        //            UserId = "47964424-53cb-431c-bdad-023379b4a7df",
        //            CreatedOn = DateTime.Now,
        //            IsDeleted = false,
        //            Likes = 4
        //        }
        //            }
        //        },
        //        new Tag()
        //        {
        //            Id = Guid.NewGuid(),
        //            Text = "gold",
        //            Photos = new List<Photo>()
        //            {
        //                new Photo
        //        {
        //            Id = Guid.NewGuid(),
        //            Path = "/Images/gold.jpg",
        //            UserId = "47964424-53cb-431c-bdad-023379b4a7df",
        //            CreatedOn = DateTime.Now,
        //            IsDeleted = false,
        //            Likes = 4
        //        }
        //            }
        //        });
        //}
    }
}

