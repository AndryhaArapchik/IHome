namespace DAL.Migrations
{
    using DAL.Entities;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.IdentityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(IdentityContext context)
        {
            context.Roles.Add(new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "admin" });
            context.Roles.Add(new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "user" });
            base.Seed(context);
        }
    }
}
