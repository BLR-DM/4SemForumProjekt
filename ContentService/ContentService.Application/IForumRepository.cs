using ContentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentService.Application
{
    public interface IForumRepository
    {
        Task AddForumAsync(Forum forum);
        Task<Forum> GetForumAsync(int id);
        Task<Forum> GetForumWithSinglePostAsync(int forumId, int postId);
        void DeleteForum(Forum forum, byte[] rowVersion);
        void UpdatePost(Post post, byte[] rowVersion);
        void DeletePost(Post post, byte[] rowVersion);
        void UpdateComment(Comment comment, byte[] rowVersion);
    }
}
