var builder = DistributedApplication.CreateBuilder(args);

var contentService = builder.AddProject<Projects.ContentService_Api>("contentservice");
var voteService = builder.AddProject<Projects.VoteService_Api>("voteservice");

builder.Build().Run();
