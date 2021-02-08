using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HelloWorldFromDB
{
    public class HelloWorldRepositoryContext : DbContext
    {
        public DbSet<Messages> Messages { get; set; }
        public HelloWorldRepositoryContext(DbContextOptions<HelloWorldRepositoryContext> options) : base(options)
        {
            if (Database.GetPendingMigrations().Any())
            {
                Database.Migrate();
            }
        }

        public HelloWorldRepositoryContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("HELLO_WORLD");
            modelBuilder.Entity<Messages>().HasKey(i => i.Author);
        }
    }

    public class Messages
    {
        [Required]
        public string Author { get; set; }

        [Required]
        public string Message { get; set; }
    }
}