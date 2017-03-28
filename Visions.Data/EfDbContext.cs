using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Visions.Data.Contracts;
using Visions.Models.Models;

namespace Visions.Data
{
    public class EfDbContext : IdentityDbContext<User>, IEfDbContext
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
    }
}

