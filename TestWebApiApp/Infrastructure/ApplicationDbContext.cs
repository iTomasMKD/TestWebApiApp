using Microsoft.EntityFrameworkCore;
using TestWebApiApp.Core.Models;

namespace TestWebApiApp.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        // Add DbSet properties for other entities as needed

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your entity relationships, constraints, etc.
            // ...

            base.OnModelCreating(modelBuilder);
        }
    }
}
