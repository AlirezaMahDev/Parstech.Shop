using System.Diagnostics.CodeAnalysis;

var builder = DistributedApplication.CreateBuilder(args);

var seq = builder.AddSeq("seq")
    .WithContainerName("seq")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataBindMount("../../.cache/seq")
    .WithEnvironment("ACCEPT_EULA", "Y");

var cache = builder.AddRedis("cache")
    .WithContainerName("redis")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataBindMount("../../.cache/redis")
    .WithDbGate(resourceBuilder =>
        resourceBuilder
            .WithContainerName("dbgate")
            .WithDataBindMount("../../.cache/dbgate")
            .WithLifetime(ContainerLifetime.Persistent));

var sql = builder.AddSqlServer("sql")
    .WithContainerName("mssql")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataBindMount("../../.cache/mssql")
    .WithDbGate();

var db = sql.AddDatabase("database");

var mongo = builder.AddMongoDB("mongo")
    .WithContainerName("mongo")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataBindMount("../../.cache/mongo")
    .WithDbGate();

var mongodb = mongo.AddDatabase("mongodb");

var ollama = builder.AddOllama("ollama")
    .WithContainerName("ollama")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .WithOpenWebUI(options => options
        .WithContainerName("webui")
        .WithDataVolume()
        .WithLifetime(ContainerLifetime.Persistent));

var model = ollama.AddHuggingFaceModel("model", "sentence-transformers/paraphrase-multilingual-mpnet-base-v2");

#pragma warning disable ASPIREHOSTINGPYTHON001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
var ai = builder.AddPythonApp("ai", "../Parstech.Shop.Ai","main.py")
    .WithHttpEndpoint()
    .WithExternalHttpEndpoints()
    .WithOtlpExporter();
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
    .WaitFor(apiService);

builder.Build().Run();