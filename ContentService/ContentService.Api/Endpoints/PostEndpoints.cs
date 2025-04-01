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
                async (IForumCommand command, CreatePostDto postDto, string appUserId, int forumId) =>
                {
                    var username = "Lucas MacQ";
                    await command.CreatePostAsync(postDto, username, appUserId, forumId);
                    return Results.Ok();
                }).WithTags(tag);

            app.MapPut("/forum/{forumId}/post/{postId}",
                async (IForumCommand command, UpdatePostDto postDto, string appUserId, int forumId, int postId) =>
                {
                    var username = "Lucas MacQ";
                    await command.UpdatePostAsync(postDto, appUserId, forumId, postId);
                    return Results.Ok(postDto);
                }).WithTags(tag);

            app.MapDelete("/forum/{forumId}/post/{postId}",
                async (IForumCommand command, DeletePostDto postDto, string appUserId, int forumId, int postId) =>
                {
                    await command.DeletePostAsync(postDto, appUserId, forumId, postId);
                    return Results.Ok();
                }).WithTags(tag);
        }
    }
}
