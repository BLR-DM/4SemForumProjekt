using ContentService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
