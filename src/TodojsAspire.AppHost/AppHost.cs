var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.TodojsAspire_ApiService>("apiservice")
    .WithHttpHealthCheck("/health");

// AddViteApp comes from community-toolkit
// use `aspire add node` and select 'ct-extensions'
builder.AddViteApp(name: "todo-frontend", workingDirectory: "../todo-frontend")
    .WithNpmPackageInstallation();

builder.Build().Run();
