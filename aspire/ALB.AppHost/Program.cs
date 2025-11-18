var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithPgWeb()
    .WithLifetime(ContainerLifetime.Persistent);

var postgresdb = postgres.AddDatabase("postgresdb");

builder.AddProject<Projects.ALB_Api>("Api")
    .WithReference(postgresdb)
    .WaitFor(postgresdb);

builder.Build().Run();