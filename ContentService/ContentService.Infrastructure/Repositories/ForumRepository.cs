using ContentService.Application;
using ContentService.Domain.Entities;

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

        void IForumRepository.DeleteForum(Forum forum, byte[] rowVersion)
        {
            throw new NotImplementedException();
        }

        void IForumRepository.DeletePost(Post post, byte[] rowVersion)
        {
            throw new NotImplementedException();
        }

        Task<Forum> IForumRepository.GetForumAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<Forum> IForumRepository.GetForumWithSinglePostAsync(int forumId, int postId)
        {
            throw new NotImplementedException();
        }

        void IForumRepository.UpdateComment(Comment comment, byte[] rowVersion)
        {
            throw new NotImplementedException();
        }

        void IForumRepository.UpdatePost(Post post, byte[] rowVersion)
        {
            throw new NotImplementedException();
        }
    }
}