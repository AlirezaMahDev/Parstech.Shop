var builder = DistributedApplication.CreateBuilder(args);

var mssql = builder.AddSqlServer("mssql").WithDataVolume();
var database = mssql.AddDatabase("database", "parstech");

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.Parstech_Shop_ApiService>("apiservice")
    .WithReference(database)
    .WaitFor(database)
    .WithReference(cache)
    .WaitFor(cache);

var web = builder.AddProject<Projects.Parstech_Shop_Web>("web")
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

var webadmin = builder.AddProject<Projects.Parstech_Shop_Web_Admin>("webadmin")
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(database)
    .WaitFor(database);

builder.AddProject<Projects.Parstech_Shop_Proxy>("proxy")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(web)
    .WithReference(webadmin);

builder.Build().Run();