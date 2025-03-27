using ContentSafetyService.Application;
using ContentSafetyService.Application.Commands;
using ContentSafetyService.Domain.Enums;
using ContentSafetyService.Domain.ValueObjects;
using ContentSafetyService.Infrastructure;
using Dapr;
using Dapr.AppCallback.Autogen.Grpc.v1;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAspire", builder =>
    {
        builder.WithOrigins("https://localhost:7214")
            .WithOrigins("http://localhost:5041")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddHttpClient();

builder.Services.AddDaprClient();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Enable DI validation at startup
builder.Services.AddOptions<ServiceProviderOptions>()
    .Configure(options => options.ValidateOnBuild = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();
}

app.UseHttpsRedirection(); 

app.UseCors("AllowAspire");

app.UseCloudEvents();

app.MapGet("/hello", () => "Hello World!");

app.MapPost("/contentmoderation",
    async (Content content, IContentSafetyCommand command) =>
    {
        var mediaType = MediaType.Text;
        var blocklists = Array.Empty<string>();

        var decision = await command.ModerateContentAsync(mediaType, content.Text, blocklists);

        return Results.Ok(decision);
    }).WithTopic("pubsub", "contentmoderation");

app.Run();
