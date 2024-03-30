using Backend.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<UserReviewPlus> UserReviewPluses { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Author>().ToTable("author");
            modelBuilder.Entity<Book>().ToTable("book");
            modelBuilder.Entity<Genre>().ToTable("genre");
            modelBuilder.Entity<Review>().ToTable("review");
            modelBuilder.Entity<UserReviewPlus>().ToTable("user_review_plus");
            modelBuilder.Entity<UserRefreshToken>().ToTable("user_refresh_token");
            
            modelBuilder.Entity<User>(b =>
            {
                b.ToTable("user");
            });

            modelBuilder.Entity<IdentityUserClaim<int>>(b =>
            {
                b.ToTable("user_claim");
            });

            modelBuilder.Entity<IdentityUserLogin<int>>(b =>
            {
                b.ToTable("user_login");
            });

            modelBuilder.Entity<IdentityUserToken<int>>(b =>
            {
                b.ToTable("user_token");
            });

            modelBuilder.Entity<IdentityRole<int>>(b =>
            {
                b.ToTable("role");
            });

            modelBuilder.Entity<IdentityRoleClaim<int>>(b =>
            {
                b.ToTable("role_claim");
            });

            modelBuilder.Entity<IdentityUserRole<int>>(b =>
            {
                b.ToTable("user_role");
            });
        }
    }
}