var builder = DistributedApplication.CreateBuilder(args);

var mssql = builder.AddSqlServer("mssql")
    .WithDataVolume();
var database = mssql.AddDatabase("database");

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.Parstech_Shop_ApiService>("apiservice")
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