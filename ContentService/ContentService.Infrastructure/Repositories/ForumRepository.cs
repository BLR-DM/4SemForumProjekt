using ContentService.Application;
using ContentService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContentService.Infrastructure.Repositories
{
    public class ForumRepository : IForumRepository
    {
        private readonly ContentContext _db;

        public ForumRepository(ContentContext db)
        {
            _db = db;
        }

        async Task IForumRepository.AddForumAsync(Forum forum)
        {
            await _db.Forums.AddAsync(forum);
            await _db.SaveChangesAsync();
        }

        async Task IForumRepository.UpdateForumAsync(Forum forum, uint rowVersion)
        {
            _db.Entry(forum).Property(nameof(forum.RowVersion)).OriginalValue = rowVersion;
            await _db.SaveChangesAsync();
        }

        void IForumRepository.DeleteForum(Forum forum, uint rowVersion)
        {
            throw new NotImplementedException();
        }

        void IForumRepository.DeletePost(Post post, uint rowVersion)
        {
            throw new NotImplementedException();
        }

        async Task<Forum> IForumRepository.GetForumAsync(int forumId)
        {
            try
            {
                return await _db.Forums
                    .Include(f => f.Posts)
                    .FirstAsync(forum => forum.Id == forumId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task<Forum> IForumRepository.GetForumWithSinglePostAsync(int forumId, int postId)
        {
            return await _db.Forums
                .Include(f => f.Posts)
                .ThenInclude(p => p.Comments)
                .Include(f => f.Posts)
                .SingleAsync(f => f.Id == forumId);
        }

        void IForumRepository.UpdateComment(Comment comment, uint rowVersion)
        {
            throw new NotImplementedException();
        }

        void IForumRepository.UpdatePost(Post post, uint rowVersion)
        {
            throw new NotImplementedException();
        }

        async Task IForumRepository.SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}