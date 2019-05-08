using AltaRail.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace AltaRail.Repository.EF.InMemory
{
    public class GenericContext : DbContext
    {
        public GenericContext(DbContextOptions<GenericContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(new User {
                Id = Guid.NewGuid().ToString(),
                Lastname = "admin",
                Name = "admin",
                Password = "abc123", // this password should be hashed
                Username = "admin"
            });

            base.OnModelCreating(builder);
        }
    }
}
