﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Linq;
using Visions.Auth;
using Visions.Data.Contracts;

namespace Visions.Data
{
    public class VisionsDbContext : IdentityDbContext<ApplicationUser>, IVisionsDbContext
    {
        public VisionsDbContext()
            : base("Visions")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<VisionsDbContext>());
        }

        public static VisionsDbContext Create()
        {
            return new VisionsDbContext();
        }

        public void InitializeDb()
        {
            //this.InitializeIdentity();
            //this.SaveChanges();
        }

        public void InitializeIdentity()
        {
            //if (!this.Users.Any())
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

            //    // Create test users
            //    User user = userManager.FindByName("admin");
            //    if (user == null)
            //    {
            //        User newUser = new User()
            //        {
            //            UserName = "admin",
            //            Email = "xxx@xxx.com"
            //        };

            //        userManager.Create(newUser, "admin");
            //        userManager.SetLockoutEnabled(newUser.Id, false);
            //        userManager.AddToRole(newUser.Id, "Admin");
            //    }
            //}
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<IdentityRole>().ToTable("AspNetRoles");
            //modelBuilder.Entity<IdentityUserRole>().ToTable("AspNetUserRoles");
            //modelBuilder.Entity<IdentityUserLogin>().ToTable("AspNetUserLogins");
            //modelBuilder.Entity<IdentityUserClaim>().ToTable("AspNetUserClaims");

            base.OnModelCreating(modelBuilder);
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}