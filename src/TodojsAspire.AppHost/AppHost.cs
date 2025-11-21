var builder = DistributedApplication.CreateBuilder(args);

// Add the following line to configure the Docker Compose environment
builder.AddDockerComposeEnvironment("env");

var sqlserver = builder.AddSqlServer("todosqlserver")
    .WithLifetime(ContainerLifetime.Persistent);

var tododb = sqlserver.AddDatabase("tododb");

var migrationService = builder.AddProject<Projects.TodojsAspire_MigrationService>("todomigration")
    .WithReference(tododb)
    .WaitFor(tododb);

var apiService = builder.AddProject<Projects.TodojsAspire_ApiService>("todoapiservice")
    .WithReference(tododb)
    .WaitForCompletion(migrationService)
    .WithHttpHealthCheck("/health")
    .WithExternalHttpEndpoints();

// use `aspire add javascript` for `AddViteApp`
var frontend = builder.AddViteApp("todofrontend", "../todo-frontend")
    .WithReference(apiService)
    .WaitFor(apiService);

apiService.PublishWithContainerFiles(frontend, "./wwwroot");

builder.Build().Run();
