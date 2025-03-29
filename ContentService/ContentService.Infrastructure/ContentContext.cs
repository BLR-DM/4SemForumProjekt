using ContentService.Domain;
using ContentService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContentService.Infrastructure
{
    public class ContentContext : DbContext
    {
        public ContentContext(DbContextOptions<ContentContext> options) : base(options)
        {
        }
        
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Forum> Forums { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Concurrency token for RowVersion (xmin)

            // Forum
            modelBuilder.Entity<Forum>()
                .Property(x => x.RowVersion)
                .IsConcurrencyToken();
            modelBuilder.Entity<Forum>()
                .Property(f => f.Status)
                .HasConversion<string>(); // Stores enum as a string

            // Post
            modelBuilder.Entity<Post>()
                .Property(x => x.RowVersion)
                .IsConcurrencyToken();

            // Comment
            modelBuilder.Entity<Comment>()
                .Property(x => x.RowVersion)
                .IsConcurrencyToken();

            

            // Table mappings on entities
            modelBuilder.Entity<Post>().ToTable("Posts");
            modelBuilder.Entity<Comment>().ToTable("Comments");
            modelBuilder.Entity<Forum>().ToTable("Forums");

            modelBuilder.Entity<Forum>()
                .Property(f => f.CreatedDate)
                .HasColumnType("timestamp(0) without time zone");
        }
    }
}
