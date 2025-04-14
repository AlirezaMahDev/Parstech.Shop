var builder = DistributedApplication.CreateBuilder(args);

var seq = builder.AddSeq("seq")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithEnvironment("ACCEPT_EULA", "Y");

var cache = builder.AddRedis("cache")
    .WithDataVolume()
    .WithDbGate(resourceBuilder => resourceBuilder.WithLifetime(ContainerLifetime.Persistent));

var sql = builder.AddSqlServer("sql")
                 .WithLifetime(ContainerLifetime.Persistent)
                 .WithDataVolume()
                 .WithDbGate();

var db = sql.AddDatabase("database");

var mongo = builder.AddMongoDB("mongo")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume()
    .WithDbGate();
    //.WithMongoExpress();

var mongodb = mongo.AddDatabase("mongodb");

var ai = builder.AddProject<Projects.Parstech_Shop_Ai>("ai");

var apiService = builder.AddProject<Projects.Parstech_Shop_ApiService>("apiservice")
    .WithReference(seq)
    .WaitFor(seq)
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(db)
    .WaitFor(db)
    .WithReference(mongodb)
    .WaitFor(mongodb);

builder.AddProject<Projects.Parstech_Shop_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(seq)
    .WaitFor(seq)
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);


builder.Build().Run();
