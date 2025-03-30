
using ContentService.Application.Commands.CommandDto.CommentDto;
using ContentService.Application.Commands.Interfaces;

namespace ContentService.Api.Endpoints
{
    public static class CommentEndpoints
    {
        public static void MapCommentEndpoints(this WebApplication app)
        {
            const string tag = "Comment";

            app.MapPost("/forum/{forumId}/post/{postId}/comment", 
                async (IPostCommand command, CreateCommentDto commentDto, int forumId, int postId, string appUserId) =>
                {
                    var username = "Bilal";
                    await command.CreateCommentAsync(commentDto, username, postId, appUserId, forumId);
                    return Results.Created();
                }).WithTags(tag);
        }
    }
}
