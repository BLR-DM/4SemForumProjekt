var builder = DistributedApplication.CreateBuilder(args);

var contentService = builder.AddProject<Projects.ContentService_Api>("contentservice");

var voteService = builder.AddProject<Projects.VoteService_Api>("voteservice");

var subscriptionService = builder.AddProject<Projects.SubscriptionService_Api>("subscriptionservice");

var contentSafetyService = builder.AddProject<Projects.ContentSafetyService_Api>("contentsafetyservice");

builder.Build().Run();
