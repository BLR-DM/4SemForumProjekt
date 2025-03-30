using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using SubscriptionService.Application.Commands.CommandDto;
using SubscriptionService.Application.Commands.Interfaces;
using SubscriptionService.Application.Configuration;
using SubscriptionService.Application.Queries.Interfaces;
using SubscriptionService.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAspire", builder =>
    {
        builder.WithOrigins("https://localhost:7018")
            .WithOrigins("http://localhost:5014")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();
}

//app.UseHttpsRedirection();

app.UseCors("AllowAspire");

app.MapGet("/hello", () => "Hello World!");

app.MapPost("/Forums/{forumId}/Subscriptions",
    async (int forumId, [FromBody] CreateSubDto subDto, IForumSubCommand command) =>
    {
        try
        {
            await command.CreateAsync(forumId, subDto.AppUserId);

            return Results.Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem(ex.Message);
        }
    });

app.MapPost("/Posts/{postId}/Subscriptions",
    async (int postId, [FromBody] CreateSubDto subDto, IPostSubCommand command) =>
    {
        try
        {
            await command.CreateAsync(postId, subDto.AppUserId);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem(ex.Message);
        }
    });

app.MapDelete("/Forums/{forumId}/Subscriptions",
    async (int forumId, [FromBody] CreateSubDto subDto, IForumSubCommand command) =>
    {
        try
        {
            await command.DeleteAsync(forumId, subDto.AppUserId);

            return Results.Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem(ex.Message);
        }
    });

app.MapDelete("/Posts/{postId}/Subscriptions",
    async (int postId, [FromBody] CreateSubDto subDto, IPostSubCommand command) =>
    {
        try
        {
            await command.DeleteAsync(postId, subDto.AppUserId);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem(ex.Message);
        }
    });

app.MapGet("/Forum/{forumId}/Subscriptions/", async (int forumId, IForumSubQuery query) =>
{
    try
    {
        var appUserIds = await query.GetSubscriptionsByForumIdAsync(forumId);

        return Results.Ok(appUserIds);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return Results.Problem(ex.Message);
    }
});

app.MapGet("/Users/{appUserId}/Forums/Subscriptions/", async (string appUserId, IForumSubQuery query) =>
{
    try
    {
        var forumIds = await query.GetSubscriptionsByUserIdAsync(appUserId);

        return Results.Ok(forumIds);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return Results.Problem(ex.Message);
    }
});

app.MapGet("/Post/{postId}/Subscriptions/", async (int postId, IPostSubQuery query) =>
{
    try
    {
        var appUserIds = await query.GetSubscriptionsByPostIdAsync(postId);
        return Results.Ok(appUserIds);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return Results.Problem(ex.Message);
    }
});

app.MapGet("/Users/{appUserId}/Posts/Subscriptions/", async (string appUserId, IPostSubQuery query) =>
{
    try
    {
        var forumIds = await query.GetSubscriptionsByUserIdAsync(appUserId);

        return Results.Ok(forumIds);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return Results.Problem();
    }
});


app.Run();