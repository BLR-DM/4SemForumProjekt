using ContentService.Application.Commands.CommandDto.CommentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentService.Application.Commands.Interfaces
{
    public interface IPostCommand
    {
        Task CreateCommentAsync(CreateCommentDto commentDto, string username, int postId, string appUserId, int forumId);
        Task UpdateCommentAsync(UpdateCommentDto commentDto, string appUserId, int forumId, int postId, int commentId);
    }
}
