using ContentService.Application;
using ContentService.Application.Commands.CommandDto.ForumDto;
using ContentService.Application.Commands.Interfaces;
using ContentService.Infrastructure;
using Dapr;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAspire", builder =>
    {
        builder.WithOrigins("https://localhost:7293")
            .WithOrigins("http://localhost:5012")
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
//app.UseCloudEvents();  <= Use?
app.MapSubscribeHandler();


app.MapGet("/hello", () => "Hello World!");


///// Endpoint verbs forum/... or need to configure CloudEvents payload etc.

app.MapPost("/forum", async (IForumCommand command, CreateForumDto forumDto, int creatorId) =>
{
    await command.CreateForumAsync(forumDto, creatorId);
    return Results.Ok();
});

app.MapPost("/forum/approved", [Topic("pubsub", "forumApproved")]
    async (IForumCommand command, PublishForumDto forumDto) =>
{
    await command.HandleApprovalAsync(forumDto);
    return Results.Ok();
});

app.MapPost("/forum/published", [Topic("pubsub", "forumToPublish")]
async (IForumCommand command, PublishForumDto forumDto) =>
{
    await command.HandlePublishAsync(forumDto);
    return Results.Ok();
});



app.Run();

