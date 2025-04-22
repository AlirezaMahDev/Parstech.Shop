var builder = DistributedApplication.CreateBuilder(args);

var seq = builder.AddSeq("seq")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataBindMount("../../.cache/seq")
    .WithEnvironment("ACCEPT_EULA", "Y");

var cache = builder.AddRedis("cache")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataBindMount("../../.cache/redis")
    .WithDbGate(resourceBuilder =>
        resourceBuilder
            .WithDataBindMount("../../.cache/dbGate")
            .WithLifetime(ContainerLifetime.Persistent));

var sql = builder.AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataBindMount("../../.cache/mssql")
    .WithBindMount("../../Data/Parstech.bak", "/var/opt/mssql/backup/Parstech.bak")
    .WithDbGate();

var db = sql.AddDatabase("database", "Parstech")
    .WithCreationScript("""
                        RESTORE DATABASE Parstech
                        FROM DISK = '/var/opt/mssql/backup/Parstech.bak'
                        WITH REPLACE, RECOVERY;
                        """);

var mongo = builder.AddMongoDB("mongo")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataBindMount("../../.cache/mongo")
    .WithDbGate();

var mongodb = mongo.AddDatabase("mongodb");

// var ollama = builder.AddOllama("ollama")
//     .WithDataVolume()
//     .WithLifetime(ContainerLifetime.Persistent)
//     .WithOpenWebUI(options => options
//         .WithDataVolume()
//         .WithLifetime(ContainerLifetime.Persistent));
//
// var model = ollama
//     .AddHuggingFaceModel("model", "sentence-transformers/paraphrase-multilingual-mpnet-base-v2");

#pragma warning disable ASPIREHOSTINGPYTHON001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
var ai = builder.AddUvicornApp("ai", "../Parstech.Shop.Ai", "main.py")
    .WithHttpEndpoint()
    .WithExternalHttpEndpoints()
    .WithOtlpExporter()
    // .WithReference(model)
    // .WaitFor(model)
    .WithReference(db)
    .WaitFor(db);
#pragma warning restore ASPIREHOSTINGPYTHON001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

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
    .WaitFor(apiService)
    .WithReference(ai)
    .WaitFor(ai);


builder.AddProject<Projects.Shop_Web>("web")
    .WithExternalHttpEndpoints()
    .WithReference(seq)
    .WaitFor(seq)
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(db)
    .WaitFor(db)
    .WithReference(mongodb)
    .WaitFor(mongodb)
    .WithReference(ai)
    .WaitFor(ai);

builder.AddDockerComposePublisher();

builder.Build().Run();