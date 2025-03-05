var builder = DistributedApplication.CreateBuilder(args);

var contentService = builder.AddProject<Projects.ContentService_Api>("contentservice");

builder.Build().Run();
