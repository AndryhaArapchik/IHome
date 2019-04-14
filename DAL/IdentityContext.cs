using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using DAL.Entities;
using System.Data.Entity.ModelConfiguration;
using System;

namespace DAL
{
    public class IdentityContext : IdentityDbContext<User>
    {
        public IdentityContext() : base("DBConnection") { }

        public static IdentityContext Create()
        {
            return new IdentityContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<IdentityRole>().ToTable("Role");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
        }
    }
}