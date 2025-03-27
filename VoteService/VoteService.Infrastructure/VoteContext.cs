using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VoteService.Domain.Entities;

namespace VoteService.Infrastructure
{
    public class VoteContext : DbContext
    {
        public VoteContext(DbContextOptions<VoteContext> options) : base(options) { }

        public DbSet<PostVote> PostVotes { get; set; }   
        public DbSet<CommentVote> CommentVotes { get; set; }
    }
}
