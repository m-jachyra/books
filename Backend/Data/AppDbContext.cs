using Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Author>().ToTable("author");
            builder.Entity<Book>().ToTable("book");
            builder.Entity<Genre>().ToTable("genre");
            builder.Entity<Review>().ToTable("review");
            builder.Entity<User>().ToTable("user");
        }
    }
}