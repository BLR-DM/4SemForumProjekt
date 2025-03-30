using ContentService.Api.Endpoints;
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
builder.Services.AddEndpointsApiExplorer();
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

/* Flow:
ContentService => contentSubmitted => ContentSafetyService moderates => contentApproved => ContentService saves => contentToPublish => ContentService saves => contentPublished

With workflow:
// High-Level Overview //

1. User Creates Forum → ContentService receives request.

2. Triggers Workflow → WorkflowService orchestrates the process.

3. Runs Content Safety Checks → ContentSafetyService validates content.

4. Updates State Store → If approved, ContentService persists forum.

5. Workflow Completes → Final status is updated, and forum is published.

*/
app.MapForumEndpoints();
app.MapPostEndpoints();
app.MapCommentEndpoints();

app.Run();

