using ContentService.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
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

app.MapGet("/hello", () => "Hello World!");

app.Run();

