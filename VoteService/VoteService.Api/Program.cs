using Scalar.AspNetCore;
using VoteService.Application;
using VoteService.Application.CommandDto.PostVoteDto;
using VoteService.Application.Interfaces;
using VoteService.Infrastructure;

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
        builder.WithOrigins("https://localhost:7070")
            .WithOrigins("http://localhost:5048")
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

app.MapPost("/postvote", async (CreatePostVoteDto dto, IPostVoteCommand command) =>
{
    try
    {
        await command.CreateVoteAsync(dto);
        return Results.Created();
    }
    catch (Exception)
    {
        return Results.BadRequest();

    }
});

app.MapGet("/hello", () => "Hello World!");

app.Run();
