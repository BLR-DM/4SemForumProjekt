using CommunityToolkit.Aspire.Hosting.Dapr;

Environment.SetEnvironmentVariable("DAPR_COMPONENTS_PATH", Path.Combine(Directory.GetCurrentDirectory(), "components"));

var builder = DistributedApplication.CreateBuilder(args);

var stateStore = builder.AddDaprStateStore("statestore");
var pubSub = builder.AddDaprPubSub("pubsub");

var contentService = builder.AddProject<Projects.ContentService_Api>("contentservice");

var voteService = builder.AddProject<Projects.VoteService_Api>("voteservice");

var subscriptionService = builder.AddProject<Projects.SubscriptionService_Api>("subscriptionservice");

var contentSafetyService = builder.AddProject<Projects.ContentSafetyService_Api>("contentsafetyservice")
    .WithDaprSidecar()
    .WithReference(stateStore)
    .WithReference(pubSub);

builder.Build().Run();
