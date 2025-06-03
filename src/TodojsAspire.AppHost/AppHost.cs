var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.TodojsAspire_ApiService>("apiservice")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.TodojsAspire_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.AddNpmApp("todo-frontend", "../todo-frontend", "dev")
    .WithReference(apiService)
    .WithHttpEndpoint(targetPort: 5173);

builder.Build().Run();
