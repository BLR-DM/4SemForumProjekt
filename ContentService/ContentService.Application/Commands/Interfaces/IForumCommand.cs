using ContentService.Application.Commands.CommandDto.ForumDto;
using ContentService.Application.Commands.CommandDto.PostDto;

namespace ContentService.Application.Commands.Interfaces
{
    public interface IForumCommand
    {
        Task CreateForumAsync(CreateForumDto forumDto);
        Task DeleteForumAsync(DeleteForumDto forumDto, int forumId);
        Task CreatePostAsync(CreatePostDto postDto, string appUserId, string role, int forumId);
        Task UpdatePostAsync(UpdatePostDto postDto, string appUserId, string role, int postId, int forumId);
        Task DeletePostAsync(DeletePostDto postDto, string appUserId, string role, int postId, int forumId);
    }
}
