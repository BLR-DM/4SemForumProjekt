using ContentService.Application.Commands.CommandDto.ForumDto;
using ContentService.Application.Commands.CommandDto.PostDto;
using ContentService.Application.Commands.Interfaces;

namespace ContentService.Api.Endpoints
{
    public static class ForumEndpoints
    {
        public static void MapForumEndpoints(this WebApplication app)
        {
            const string tag = "Forum";
            ///// Endpoint verbs forum/... or need to configure CloudEvents payload etc.
            //
            //  MapGroup remove forum prefix

            app.MapPost("/forum",
                async (IForumCommand command, CreateForumDto forumDto, string appUserId) =>
                {
                    await command.CreateForumAsync(forumDto, appUserId);
                    return Results.Created(); // Results.Accepted -> workflow start
                }).WithTags(tag);

            //app.MapPost("/forum/approved",
            //    async (IForumCommand command, PublishForumDto forumDto) =>
            //    {
            //        await command.HandleApprovalAsync(forumDto);
            //        return Results.Ok();
            //    }).WithTopic("pubsub", "forumApproved");

            //app.MapPost("/forum/publish",
            //    async (IForumCommand command, PublishForumDto forumDto) =>
            //    {
            //        await command.HandlePublishAsync(forumDto);
            //        return Results.Ok();
            //    }).WithTopic("pubsub", "forumToPublish");
        }
    }
}
