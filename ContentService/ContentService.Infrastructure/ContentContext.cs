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
            modelBuilder.Entity<Forum>()
                .Property(f => f.CreatedDate)
                .HasColumnType("timestamp(0) without time zone");
        }
    }
}
