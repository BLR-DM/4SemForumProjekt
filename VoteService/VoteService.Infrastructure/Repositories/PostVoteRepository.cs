using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VoteService.Domain.Entities;
using VoteService.Domain.Interfaces;

namespace VoteService.Infrastructure.Repositories
{
    public class PostVoteRepository : IPostVoteRepository
    {
        private readonly VoteContext _context;

        public PostVoteRepository(VoteContext context)
        {
            _context = context;
        }

        async Task IPostVoteRepository.AddPostVoteAsync(PostVote postVote)
        {
            _context.PostVotes.Add(postVote);
            await _context.SaveChangesAsync();
        }

        Task IPostVoteRepository.DeletePostVoteAsync(int id)
        {
            throw new NotImplementedException();
        }

        async Task<PostVote> IPostVoteRepository.GetVoteAsync(string userId, string postId)
        {
            return await _context.PostVotes.FirstOrDefaultAsync(v => v.UserId == userId && v.PostId == postId);
        }

        Task IPostVoteRepository.UpdateVoteAsync(PostVote postVote)
        {
            throw new NotImplementedException();
        }
    }
}
