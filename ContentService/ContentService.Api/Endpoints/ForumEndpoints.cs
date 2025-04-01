﻿using ContentService.Application.Commands.CommandDto.ForumDto;
using ContentService.Application.Commands.CommandDto.PostDto;
using ContentService.Application.Commands.Interfaces;
using ContentService.Domain.Entities;

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

            app.MapPut("/forum/{forumId}", 
                async (IForumCommand command, UpdateForumDto forumDto, string appUserId, int forumId) =>
                {
                    await command.UpdateForumAsync(forumDto, appUserId, forumId);
                    return Results.Ok(forumDto);
                }).WithTags(tag);

            app.MapDelete("/forum/{forumId}", // check appUserId / moderator
                async (IForumCommand command, DeleteForumDto forumDto, string appUserId, int forumId) =>
                {
                    await command.DeleteForumAsync(forumDto, forumId);
                    return Results.Ok("Forum deleted");
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
