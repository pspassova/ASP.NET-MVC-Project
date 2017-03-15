using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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

        public IDbSet<Article> Articles
        {
            get; set;
        }

        public IDbSet<Photo> Photos
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

        public IStateful<T> GetStateful<T>(T entity) where T : class
        {
            return this.statefulFactory.CreateStateful(base.Entry<T>(entity));
        }

        public void InitializeDb()
        {
            //this.InitializeIdentity();
            //this.SeedPhotos();
            //this.SaveChanges();
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

        private void InitializeIdentity()
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

            // Create admin
            User user = userManager.FindByEmail("admin@admin.com");
            userManager.AddToRole(user.Id, "Admin");
        }

        private void SeedPhotos()
        {
            this.Photos.AddOrUpdate(p => p.Path,
                new Photo
                {
                    Id = Guid.NewGuid(),
                    Path = "/Images/sea.jpg",
                    UserId = "e900466b-8556-414a-8e49-c8e34e9c859b",
                    CreatedOn = DateTime.Now,
                    IsDeleted = false,
                    Likes = 1,
                    Tags = new string[] { "sea", "sunset" }
                },
                new Photo
                {
                    Id = Guid.NewGuid(),
                    Path = "/Images/rain.jpg",
                    UserId = "e900466b-8556-414a-8e49-c8e34e9c859b",
                    CreatedOn = DateTime.Now,
                    IsDeleted = false,
                    Likes = 4,
                    Tags = new string[] { "sea", "rain", "woman", "happiness", "raindrops" }
                },
                new Photo
                {
                    Id = Guid.NewGuid(),
                    Path = "/Images/gold.jpg",
                    UserId = "e900466b-8556-414a-8e49-c8e34e9c859b",
                    CreatedOn = DateTime.Now,
                    IsDeleted = false,
                    Likes = 0,
                    Tags = new string[] { "gold", "purple", "sky" }
                });
        }
    }
}
