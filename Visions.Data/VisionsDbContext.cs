using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Visions.Data.Contracts;
using Visions.Data.Factories;
using Visions.Models.Models;

namespace Visions.Data
{
    public class VisionsDbContext : IdentityDbContext<User>, IVisionsDbContext
    {
        private IStatefulFactory statefulFactory;

        public VisionsDbContext()
            : base("Visions")
        {
        }

        public VisionsDbContext(IStatefulFactory statefulFactory)
            : base("Visions")
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<VisionsDbContext>());

            this.statefulFactory = statefulFactory;
        }

        public IDbSet<Article> Articles { get; set; }

        public IDbSet<Photo> Photos { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public static VisionsDbContext Create()
        {
            return new VisionsDbContext();
        }

        public IStateful<T> GetStateful<T>(T entity) where T : class
        {
            return this.statefulFactory.CreateStateful(base.Entry<T>(entity));
        }

        public void InitializeDb()
        {
            this.InitializeIdentity();
            this.SaveChanges();
        }

        public void InitializeIdentity()
        {
            var roleStore = new RoleStore<IdentityRole>(this);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userStore = new UserStore<User>(this);
            var userManager = new UserManager<User>(userStore);

            // Add missing roles
            var role = roleManager.FindByName("Admin");
            if (role == null)
            {
                role = new IdentityRole("Admin");
                roleManager.Create(role);
            }
            this.SaveChanges();
            // Create test users
            User user = userManager.FindByName("admin");
            if (user == null)
            {
                User newUser = new User()
                {
                    Email = "admin@admin.com"
                };

                userManager.Create(newUser, "admin1");
                userManager.SetLockoutEnabled(newUser.Id, false);
                userManager.AddToRole(newUser.Id, "Admin");
            }
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("AspNetUsers");
            modelBuilder.Entity<IdentityRole>().ToTable("AspNetRoles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("AspNetUserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("AspNetUserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("AspNetUserClaims");

            base.OnModelCreating(modelBuilder);
        }
    }
}
