IDistributedApplicationBuilder? builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<SqlServerServerResource>? mssql = builder.AddSqlServer("mssql")
    .WithDataVolume();
IResourceBuilder<SqlServerDatabaseResource>? database = mssql.AddDatabase("database");

IResourceBuilder<RedisResource>? cache = builder.AddRedis("cache");

IResourceBuilder<ProjectResource>? apiService = builder.AddProject<Projects.Parstech_Shop_ApiService>("apiservice")
    .WithReference(database)
    .WaitFor(database)
    .WithReference(cache)
    .WaitFor(cache);

builder.AddProject<Projects.Parstech_Shop_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();