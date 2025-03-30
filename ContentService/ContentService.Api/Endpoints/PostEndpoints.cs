using ContentService.Application.Commands.CommandDto.PostDto;
using ContentService.Application.Commands.Interfaces;

namespace ContentService.Api.Endpoints
{
    public static class PostEndpoints
    {
        public static void MapPostEndpoints(this WebApplication app)
        {
            const string tag = "Post";

            app.MapPost("/forum/{forumId}/post",
                async (IForumCommand command, CreatePostDto postDto, int forumId, string appUserId) =>
                {
                    var username = "Lucas MacQ";
                    await command.CreatePostAsync(postDto, username, appUserId, forumId);
                    return Results.Ok();
                }).WithTags(tag);
        }
    }
}
