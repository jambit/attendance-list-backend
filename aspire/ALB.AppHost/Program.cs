var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithPgWeb()
    .WithLifetime(ContainerLifetime.Persistent);

var postgresdb = postgres.AddDatabase("postgresdb");

var migrationService = builder.AddProject<Projects.ALB_MigrationService>("MigrationService")
    .WithReference(postgresdb)
    .WaitFor(postgresdb);

var api = builder.AddProject<Projects.ALB_Api>("Api")
    .WithReference(postgresdb)
    .WaitFor(postgresdb)
    .WaitFor(migrationService);

builder.AddProject<Projects.ALB_Ui>("Ui")
    .WithReference(postgresdb)
    .WaitFor(postgresdb)
    .WaitFor(migrationService)
    .WaitFor(api);

builder.Build().Run();